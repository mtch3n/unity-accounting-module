using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using AccountingModule.Data;

namespace AccountingModule
{
    public abstract class Accounting
    {
        private readonly Option _opt;
        private readonly WAL _wal;
        private Journal _memJournal;

        protected Accounting(Option option)
        {
            _opt = option;
            _wal = new WAL(option);

            Prepare();
        }

        private void Prepare()
        {
            _memJournal = LoadJournal(JournalPath());
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
            _memJournal.Beat += Sun(JournalType.Beat);

            foreach (var p in (PlayerScore[])Enum.GetValues(typeof(PlayerScore)))
            {
                var log = FindLastRecord(p.ToString());
                if (log == null) continue;

                var val = FindLastRecord(p.ToString()).Value;
                if (_memJournal.PlayerScore.ContainsKey(p))
                    _memJournal.PlayerScore[p] = BitConverter.ToInt64(val, 0);
                else
                    _memJournal.PlayerScore.Add(p, BitConverter.ToInt64(val, 0));
            }

            foreach (var p in (PlayerBet[])Enum.GetValues(typeof(PlayerBet)))
            {
                var log = FindLastRecord(p.ToString());
                if (log == null) continue;

                var val = FindLastRecord(p.ToString()).Value;
                if (_memJournal.PlayerBet.ContainsKey(p))
                    _memJournal.PlayerBet[p] = BitConverter.ToInt64(val, 0);
                else
                    _memJournal.PlayerBet[p] = BitConverter.ToInt64(val, 0);
            }

            Commit();
        }


        private WalLog FindLastRecord(string key)
        {
            return _wal.ReportLogs()
                .Where(log => log.Type == JournalType.Generic)
                .LastOrDefault(log => log.Key == key);
        }

        public void DoArchive()
        {
            Commit();
            NewJournalArchive();
            WriteBook();
            Reset();
        }

        public Journal CurrentReport()
        {
            NewJournalArchive();
            return _memJournal;
        }

        private void NewJournalArchive()
        {
            Journal lastJournal;
            if (Archive().JournalArchives.Count == 0)
            {
                lastJournal = new Journal();
            }
            else
            {
                lastJournal = Archive().JournalArchives.Last();
            }

            _memJournal.CalculateProfit(lastJournal.Profit, lastJournal.PointGain, lastJournal.PointSpend);
            _memJournal.CalculateProfitCoin(lastJournal.ProfitCoin, lastJournal.ProfitPoint);
        }

        private void WriteBook()
        {
            var book = Book.Load(ArchivePath());

            book.JournalArchives.Add(_memJournal);
            book.Write(ArchivePath());
        }

        public void Commit()
        {
            WriteJournal(JournalPath(), _memJournal.Serialize());
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
            if (type == JournalType.Generic)
                throw new InvalidCastException("cannot convert generic journal type to int");

            return _wal.ReportLogs().Where(x => x.Type == type).Sum(x => BitConverter.ToInt32(x.Value, 0));
        }

        private Journal LoadJournal(string path)
        {
            if (!File.Exists(path))
            {
                var ledger = new Journal();
                WriteJournal(path, ledger.Serialize());
                return ledger;
            }

            var fs = new FileStream(path, FileMode.Open);
            var formatter = new BinaryFormatter();
            var l = (Journal)formatter.Deserialize(fs);

            fs.Close();
            return l;
        }

        private void WriteJournal(string path, byte[] data)
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

        public Journal Journal()
        {
            return _memJournal;
        }

        public long Bet(PlayerBet player)
        {
            if (!_memJournal.PlayerBet.ContainsKey(player))
                return 0;

            return _memJournal.PlayerBet[player];
        }

        public long Score(PlayerScore player)
        {
            if (!_memJournal.PlayerScore.ContainsKey(player))
                return 0;

            return _memJournal.PlayerScore[player];
        }

        public Book Archive()
        {
            return Book.Load(ArchivePath());
        }

        private WalLog NewLogEntry(JournalType type, int value)
        {
            return new WalLog
            {
                Type = type,
                Value = BitConverter.GetBytes(value)
            };
        }

        private WalLog NewLogEntry(string key, byte[] value)
        {
            return new WalLog
            {
                Type = JournalType.Generic,
                Key = key,
                Value = value
            };
        }

        private void Append(WalLog log)
        {
            if (_wal.Count() >= _opt.CommitThreshold)
                if (!_opt.NoCommit)
                    Commit();

            _wal.Append(log);
        }

        #region PathUtils

        private string JournalPath()
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

        public void LogBeat(int value)
        {
            Append(NewLogEntry(JournalType.Beat, value));
            _memJournal.Beat += value;
        }

        public void LogScore(PlayerScore player, long value)
        {
            Append(NewLogEntry(player.ToString(), BitConverter.GetBytes(value)));
            _memJournal.PlayerScore[player] = value;
        }

        public void LogBet(PlayerBet player, long bet)
        {
            Append(NewLogEntry(player.ToString(), BitConverter.GetBytes(bet)));
            _memJournal.PlayerBet[player] = bet;
        }

        #endregion
    }
}