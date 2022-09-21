using System;
using System.Linq;
using AccountingModule.Data;

namespace AccountingModule
{
    public class Report : Accounting
    {
        public Report(Option option) : base(option)
        {
        }

        #region current

        public long CurrentOpen()
        {
            return CurrentReport().Open;
        }

        public long CurrentWash()
        {
            return CurrentReport().Wash;
        }

        public long CurrentInsertCoin()
        {
            return CurrentReport().InsertCoin;
        }

        public long CurrentRefundCoin()
        {
            return CurrentReport().RefundCoin;
        }

        public long CurrentProfitPoint()
        {
            return CurrentReport().ProfitPoint;
        }

        public long CurrentCoinProfit()
        {
            return CurrentReport().ProfitCoin;
        }

        public long CurrentGameBeat()
        {
            return CurrentReport().Beat;
        }

        #endregion

        #region CurrentTotal

        public long ProfitTotal()
        {
            return CurrentReport().Profit + PreviousJournalArchives().Profit;
        }

        public long PreviousProfit()
        {
            return PreviousJournalArchives().Profit;
        }

        public long CurrentProfit()
        {
            return CurrentReport().Profit;
        }

        #endregion

        #region previous

        private Journal PreviousJournalArchives()
        {
            try
            {
                return Archive().JournalArchives.Last();
            }
            catch (InvalidOperationException)
            {
                return new Journal();
            }
        }

        public long PreviousOpen()
        {
            return PreviousJournalArchives().Open;
        }

        public long PreviousWash()
        {
            return PreviousJournalArchives().Wash;
        }

        public long PreviousInsertCoin()
        {
            return PreviousJournalArchives().InsertCoin;
        }

        public long PreviousRefundCoin()
        {
            return PreviousJournalArchives().RefundCoin;
        }

        public long PreviousProfitPoint()
        {
            return PreviousJournalArchives().ProfitPoint;
        }

        public long PreviousCoinProfit()
        {
            return PreviousJournalArchives().ProfitCoin;
        }

        public long PreviousGameBeat()
        {
            return PreviousJournalArchives().Beat;
        }

        #endregion

        #region util

        public void RunTime()
        {
        }

        public void GameTime()
        {
        }

        public void LeftTime()
        {
        }

        public int ReportCount()
        {
            return Archive().JournalArchives.Count;
        }

        #endregion

        #region Total

        public long TotalProfit()
        {
            return ProfitTotal();
        }

        public long TotalPreviousProfit()
        {
            return PreviousProfit();
        }

        public long TotalCurrentProfit()
        {
            return CurrentProfit();
        }

        public long AllOpen()
        {
            var v = Archive().JournalArchives.Sum(x => x.Open);
            return v + CurrentReport().Open;
        }

        public long AllWash()
        {
            var v = Archive().JournalArchives.Sum(x => x.Wash);
            return v + CurrentReport().Wash;
        }

        public long AllInsertCoin()
        {
            var v = Archive().JournalArchives.Sum(x => x.InsertCoin);
            return v + CurrentReport().InsertCoin;
        }

        public long AllRefundCoin()
        {
            var v = Archive().JournalArchives.Sum(x => x.RefundCoin);
            return v + CurrentReport().RefundCoin;
        }

        public long AllSpend()
        {
            var v = Archive().JournalArchives.Sum(x => x.PointSpend);
            return v + CurrentReport().PointSpend;
        }

        public long AllGain()
        {
            var v = Archive().JournalArchives.Sum(x => x.PointGain);
            return v + CurrentReport().PointGain;
        }

        public long AllThousandths()
        {
            return AllGain() / AllSpend() * 1000;
        }

        public long AllBeat()
        {
            var v = Archive().JournalArchives.Sum(x => x.Beat);
            return v + CurrentReport().Beat;
        }

        #endregion
    }
}