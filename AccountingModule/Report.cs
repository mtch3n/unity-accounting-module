using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using AccountingModule.Data;

namespace AccountingModule
{
    public class Report
    {
        private readonly Option _opt;
        private readonly WAL _wal;
        private Ledger _memLedger;

        public Report(Option option)
        {
            _opt = option;
            _wal = new WAL(option);

            Prepare();
        }

        private void Prepare()
        {
            _memLedger = LoadLedger(CommitPath());
            _wal.Open();

            if (_opt.DiscardLogs) Discard();

            Restore();
        }

        private void Restore()
        {
            _memLedger.Open += Sun(ReportType.Open);
            _memLedger.Wash += Sun(ReportType.Wash);
            _memLedger.InsertCoin += Sun(ReportType.InsertCoin);
            _memLedger.RefundCoin += Sun(ReportType.RefundCoin);
            _memLedger.PointGain += Sun(ReportType.PointGain);
            _memLedger.PointSpend += Sun(ReportType.PointSpend);

            Commit();
        }

        #region PathUtils

        private string CommitPath()
        {
            return Path.GetFullPath(_opt.Path + "/data.bin");
        }

        private string ArchivePath()
        {
            return Path.GetFullPath(_opt.Path + "/archive.bin");
        }

        #endregion

        public void Archive()
        {
            Commit();

            var newArchive = NewLedgerArchive();
            WriteLedger(ArchivePath(), newArchive.Serialize());

            Reset();
        }

        private Ledger NewLedgerArchive()
        {
            var ledgerHistory = LoadLedger(ArchivePath());
            ledgerHistory.Open += _memLedger.Open;
            ledgerHistory.Wash += _memLedger.Wash;
            ledgerHistory.InsertCoin += _memLedger.InsertCoin;
            ledgerHistory.PointGain += _memLedger.PointGain;
            ledgerHistory.PointSpend += _memLedger.PointSpend;
            ledgerHistory.RefundCoin += _memLedger.RefundCoin;

            return ledgerHistory;
        }

        private void Commit()
        {
            WriteLedger(CommitPath(), _memLedger.Serialize());
            Discard();
        }

        private void Discard()
        {
            _wal.Flush();
        }

        public void Reset()
        {
            _memLedger = new Ledger();

            Discard();
            Commit();
        }

        private int Sun(ReportType type)
        {
            return _wal.ReportLogs().Where(x => x.Type == type).Sum(x => x.Value);
        }

        private Ledger LoadLedger(string path)
        {
            if (!File.Exists(path))
            {
                var ledger = new Ledger();
                WriteLedger(CommitPath(), ledger.Serialize());
                return ledger;
            }

            var fs = new FileStream(path, FileMode.Open);
            var formatter = new BinaryFormatter();
            var l = (Ledger)formatter.Deserialize(fs);

            fs.Close();
            return l;
        }

        private void WriteLedger(string path, byte[] data)
        {
            using (var fileStream =
                   new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
            {
                using (var bw = new BinaryWriter(fileStream))
                {
                    bw.Write(data);
                }
            }
        }

        public Ledger GetLedger()
        {
            return _memLedger;
        }

        public Ledger GetArchive()
        {
            return LoadLedger(ArchivePath());
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
            if (_wal.Count() >= _opt.CommitThreshold)
                if (!_opt.NoCommit)
                    Commit();

            _wal.Append(log);
        }

        #region LogReport

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

        #endregion
    }
}