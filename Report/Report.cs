using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Report.Data;

namespace Report
{
    public class Report
    {
        private int _index;
        private Ledger _memLedger;
        private readonly Option _opt;
        private readonly WAL _wal;

        public Report(Option option)
        {
            _opt = option;
            _wal = new WAL(option);

            PrepareWAL();
        }

        private void PrepareWAL()
        {
            _wal.Open();
        }
        
        private string CommitPath()
        {
            return Path.GetFullPath(_opt.Path + "/commit.dat");
        }

        public void Commit()
        {
            var ledger = LoadLedger();

            ledger.Open += Sun(ReportType.Open);
            ledger.Wash += Sun(ReportType.Wash);
            ledger.InsertCoin += Sun(ReportType.InsertCoin);
            ledger.RefundCoin += Sun(ReportType.RefundCoin);
            ledger.PointGain += Sun(ReportType.PointGain);
            ledger.PointSpend += Sun(ReportType.PointSpend);

            WriteLedger(ledger.Serialize());

            RollOver();
        }

        private void RollOver()
        {
            _index = 0;
            _wal.Flush();
        }

        private int Sun(ReportType type)
        {
            var reportLogs = _wal.GetReportLogs();
            return reportLogs.Where(x => x.Type == type).Sum(x => x.Value);
        }

        private Ledger LoadLedger()
        {
            if (!File.Exists(CommitPath())) CreateNewLedger();

            var fs = new FileStream(CommitPath(), FileMode.Open);
            var formatter = new BinaryFormatter();
            var l = (Ledger)formatter.Deserialize(fs);

            fs.Close();
            return l;
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

        public void LogOpen(int value)
        {
            Append(NewLogEntry(ReportType.Open, value));
            _memLedger.Open += value;
        }

        public void LogWash(int value)
        {
            Append(NewLogEntry(ReportType.Wash, value));
            _memLedger.Wash += value;
        }

        public void LogInsertCoin(int value)
        {
            Append(NewLogEntry(ReportType.InsertCoin, value));
            _memLedger.InsertCoin += value;
        }

        public void LogRefundCoin(int value)
        {
            Append(NewLogEntry(ReportType.RefundCoin, value));
            _memLedger.RefundCoin += value;
        }

        public void LogPointGain(int value)
        {
            Append(NewLogEntry(ReportType.PointGain, value));
            _memLedger.PointGain += value;
        }

        public void LogPointSpend(int value)
        {
            Append(NewLogEntry(ReportType.PointSpend, value));
            _memLedger.PointSpend += value;
        }

        public Ledger GetLedger()
        {
            return _memLedger;
        }

        private ReportLog NewLogEntry(ReportType type, int value)
        {
            return new ReportLog
            {
                Type = type,
                Value = value
            };
        }

        private void Append(ReportLog log)
        {
            if (_index >= _opt.CommitThreshold) Commit();

            _wal.Append(log);

            _index++;
        }
    }
}