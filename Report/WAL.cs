using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Report.Data;

namespace Report
{
    public class WAL
    {
        private const int WalLimit = 10;

        private readonly List<ReportLog> _reportLogs = new List<ReportLog>();

        private readonly Option _opt;

        private int _index;

        public WAL(Option opt)
        {
            this._opt = opt;
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
            if (_index >= WalLimit) Commit();

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

            ledger.Open += _reportLogs.Where(x => x.type == ReportType.Open).Sum(x => x.value);
            ledger.Wash += _reportLogs.Where(x => x.type == ReportType.Wash).Sum(x => x.value);
            ledger.InsertCoin += _reportLogs.Where(x => x.type == ReportType.InsertCoin).Sum(x => x.value);
            ledger.RefundCoin += _reportLogs.Where(x => x.type == ReportType.RefundCoin).Sum(x => x.value);
            ledger.PointGain += _reportLogs.Where(x => x.type == ReportType.PointGain).Sum(x => x.value);
            ledger.PointSpend += _reportLogs.Where(x => x.type == ReportType.PointSpend).Sum(x => x.value);

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

        public void Flush()
        {
        }

        // public int Read(int idx)
        // {
        //     var entry = entries[idx];
        //
        //     if (entry.type == (ushort)ReportType.Report)
        //     {
        //         var value = BitConverter.ToInt32(entry.data, 0);
        //     }
        // }

        public void Open()
        {
            var entry = new LogEntry(_opt.Path);
        }
    }

    [Serializable]
    public class ReportLog
    {
        public ReportType type;
        public int value;

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