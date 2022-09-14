using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using AccountingModule.Data;
using AccountingModule.Exceptions;

namespace AccountingModule
{
    public class WAL
    {
        private readonly Option _opt;

        private readonly List<ReportLog> _reportLogs = new List<ReportLog>();

        private readonly List<BytePositions> epos = new List<BytePositions>();

        private int _index;

        public WAL(Option opt)
        {
            _opt = opt;
        }

        private string WALPath()
        {
            return Path.GetFullPath(_opt.Path + "/wal.bin");
        }

        public void Append(ReportLog rLog)
        {
            var sData = rLog.Serialize();

            var entry = new LogEntry(WALPath())
            {
                data = sData,
                size = (ulong)sData.Length
            };

            if (_opt.MemWal)
            {
                _reportLogs.Add(rLog);
                return;
            }

            entry.AppendBinaryFile();

            _reportLogs.Add(rLog);
            _index++;
        }

        public void Flush()
        {
            _index = 0;
            _reportLogs.Clear();

            File.Delete(WALPath());
        }

        public List<ReportLog> ReportLogs()
        {
            return _reportLogs;
        }

        public int Count()
        {
            return _index;
        }

        public void Open()
        {
            if (!File.Exists(WALPath())) return;
            LoadWalSegments();
            LoadReportLogs();
        }

        public WAL Instance()
        {
            return this;
        }

        private void LoadReportLogs()
        {
            for (var i = 0; i < epos.Count; i++) _reportLogs.Add(Deserialize<ReportLog>(Read(i)));
        }

        private T Deserialize<T>(byte[] param)
        {
            using (var ms = new MemoryStream(param))
            {
                IFormatter br = new BinaryFormatter();
                return (T)br.Deserialize(ms);
            }
        }

        public byte[] Read(int index)
        {
            if (index > epos.Count) throw new IndexOutOfRangeException("Entry index out of range");

            var buf = new byte[SizeOfEntry(index)];

            using (var reader = new BinaryReader(new FileStream(WALPath(), FileMode.Open)))
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

            return epos[index].Pos + ulongLength;
        }

        private void LoadWalSegments()
        {
            var data = ReadWalFile();

            if (data.Length == 0) throw new InvalidWalException("Log entry is corrupted");

            var pos = 0;
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
        public int End; // one byte past pos
        public int Pos; // byte position

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