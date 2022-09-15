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
        private Journal _memJournal;

        public Report(Option option)
        {
            _opt = option;
            _wal = new WAL(option);

            Prepare();
        }

        private void Prepare()
        {
            _memJournal = LoadLedger(CommitPath());
            _wal.Open();

            if (_opt.DiscardLogs) Discard();

            Restore();
        }

        private void Restore()
        {
            _memJournal.Open += Sun(JournalType.Open);
            _memJournal.Wash += Sun(JournalType.Wash);
            _memJournal.InsertCoin += Sun(JournalType.InsertCoin);
            _memJournal.RefundCoin += Sun(JournalType.RefundCoin);
            _memJournal.PointGain += Sun(JournalType.PointGain);
            _memJournal.PointSpend += Sun(JournalType.PointSpend);

            Commit();
        }

        public void Archive()
        {
            Commit();
            NewLedgerArchive();
            Reset();
        }

        private void NewLedgerArchive()
        {
            var book = Book.Load(ArchivePath());

            book.JournalArchives.Add(_memJournal);
            book.Write(ArchivePath());
        }

        private void Commit()
        {
            WriteLedger(CommitPath(), _memJournal.Serialize());
            Discard();
        }

        private void Discard()
        {
            _wal.Flush();
        }

        public void Reset()
        {
            _memJournal = new Journal();
            Commit();
        }

        private int Sun(JournalType type)
        {
            return _wal.ReportLogs().Where(x => x.Type == type).Sum(x => x.Value);
        }

        private Journal LoadLedger(string path)
        {
            if (!File.Exists(path))
            {
                var ledger = new Journal();
                WriteLedger(path, ledger.Serialize());
                return ledger;
            }

            var fs = new FileStream(path, FileMode.Open);
            var formatter = new BinaryFormatter();
            var l = (Journal)formatter.Deserialize(fs);

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

        public Journal GetLedger()
        {
            return _memJournal;
        }

        public Book GetArchive()
        {
            return Book.Load(ArchivePath());
        }

        private ReportLog NewLogEntry(JournalType type, int value)
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

        #region LogReport

        public void LogOpen(int value)
        {
            Append(NewLogEntry(JournalType.Open, value));
            _memJournal.Open += value;
        }

        public void LogWash(int value)
        {
            Append(NewLogEntry(JournalType.Wash, value));
            _memJournal.Wash += value;
        }

        public void LogInsertCoin(int value)
        {
            Append(NewLogEntry(JournalType.InsertCoin, value));
            _memJournal.InsertCoin += value;
        }

        public void LogRefundCoin(int value)
        {
            Append(NewLogEntry(JournalType.RefundCoin, value));
            _memJournal.RefundCoin += value;
        }

        public void LogPointGain(int value)
        {
            Append(NewLogEntry(JournalType.PointGain, value));
            _memJournal.PointGain += value;
        }

        public void LogPointSpend(int value)
        {
            Append(NewLogEntry(JournalType.PointSpend, value));
            _memJournal.PointSpend += value;
        }

        #endregion

        #region

        #endregion
    }
}