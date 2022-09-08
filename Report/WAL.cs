using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Report.Data;
using Report.Exceptions;

namespace Report
{
    public class WAL
    {
        private const int WalLimit = 10;

        private readonly Option _opt;

        private readonly List<ReportLog> _reportLogs = new List<ReportLog>();

        private int _index;

        public WAL(Option opt)
        {
            _opt = opt;
        }

        private string WALPath()
        {
            return Path.GetFullPath(_opt.Path + "/wal.dat");
        }

        private string CommitPath()
        {
            return Path.GetFullPath(_opt.Path + "/commit.dat");
        }

        public void Append(ReportLog rLog)
        {
            if (_index >= _opt.CommitThreshold) Commit();

            var sData = rLog.Serialize();

            var entry = new LogEntry(WALPath())
            {
                data = sData,
                size = (ulong)sData.Length
            };

            entry.AppendBinaryFile();

            _reportLogs.Add(rLog);
            _index++;
        }

        public void Commit()
        {
            if (!File.Exists(CommitPath())) CreateNewLedger();

            var ledger = GetLedger();

            ledger.Open += _reportLogs.Where(x => x.Type == ReportType.Open).Sum(x => x.Value);
            ledger.Wash += _reportLogs.Where(x => x.Type == ReportType.Wash).Sum(x => x.Value);
            ledger.InsertCoin += _reportLogs.Where(x => x.Type == ReportType.InsertCoin).Sum(x => x.Value);
            ledger.RefundCoin += _reportLogs.Where(x => x.Type == ReportType.RefundCoin).Sum(x => x.Value);
            ledger.PointGain += _reportLogs.Where(x => x.Type == ReportType.PointGain).Sum(x => x.Value);
            ledger.PointSpend += _reportLogs.Where(x => x.Type == ReportType.PointSpend).Sum(x => x.Value);

            WriteLedger(ledger.Serialize());

            RollOver();
        }

        public Ledger GetLedger()
        {
            if (!File.Exists(CommitPath())) CreateNewLedger();

            var fs = new FileStream(CommitPath(), FileMode.Open);
            var formatter = new BinaryFormatter();
            var l = (Ledger)formatter.Deserialize(fs);

            fs.Close();
            return l;
        }

        private void RollOver()
        {
            _index = 0;
            _reportLogs.Clear();

            File.Delete(WALPath());
        }

        private void CreateNewLedger()
        {
            var ledger = new Ledger();
            WriteLedger(ledger.Serialize());
        }

        private void WriteLedger(byte[] data)
        {
            using (var fileStream =
                   new FileStream(CommitPath(), FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
            {
                using (var bw = new BinaryWriter(fileStream))
                {
                    bw.Write(data);
                }
            }
        }

        public void FlushOnDisk()
        {
            if (_index > 0) Commit();
        }

        public void Open()
        {
            if (!File.Exists(WALPath())) return;
            LoadWalSegments();
        }

        private List<BytePositions> epos = new List<BytePositions>();

        public byte[] Read(int index)
        {
            if (index > epos.Count)
            {
                throw new IndexOutOfRangeException("Entry index out of range");
            }


            byte[] buf = new byte[SizeOfEntry(index)];
            using (BinaryReader reader = new BinaryReader(new FileStream(WALPath(), FileMode.Open)))
            {
                reader.BaseStream.Seek(EntryDataStart(index), SeekOrigin.Begin);
                reader.Read(buf, 0, SizeOfEntry(index));
            }

            return buf;
        }

        private int SizeOfEntry(int index)
        {
            var ulongLength = sizeof(ulong);

            return epos[index].End - epos[index].Pos - ulongLength;
        }

        private int EntryDataStart(int index)
        {
            var ulongLength = sizeof(ulong);

            return epos[index].Pos + ulongLength - 1;
        }

        private void LoadWalSegments()
        {
            var data = ReadWalFile();

            if (data.Length == 0) throw new InvalidWalException("Log entry is corrupted");

            int pos = 0;
            for (_index = 0; data.Length > 0; _index++)
            {
                var n = LoadNextBinaryEntry(data);

                data = data.Skip(n).Take(data.Length - 1).ToArray();
                epos.Add(new BytePositions(pos, pos + n));
                pos += n;
            }
        }

        private int LoadNextBinaryEntry(byte[] data)
        {
            var ulongLength = sizeof(ulong);

            // data_size + data
            var size = BitConverter.ToUInt64(data.Take(ulongLength).ToArray(), 0);

            return ulongLength + (int)size;
        }

        private byte[] ReadWalFile()
        {
            using (var fileStream = new FileStream(WALPath(), FileMode.Open, FileAccess.Read))
            using (var br = new BinaryReader(fileStream))
            {
                return br.ReadBytes((int)fileStream.Length);
            }
        }
    }

    public class BytePositions
    {
        public int Pos; // byte position
        public int End; // one byte past pos

        public BytePositions(int pos, int end)
        {
            Pos = pos;
            End = end;
        }
    }

    [Serializable]
    public class ReportLog
    {
        public ReportType Type;
        public int Value;

        public byte[] Serialize()
        {
            byte[] bytes;
            IFormatter formatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, this);
                bytes = stream.ToArray();
            }

            return bytes;
        }
    }
}