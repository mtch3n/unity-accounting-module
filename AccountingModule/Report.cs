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
            return Journal().Open;
        }

        public long CurrentWash()
        {
            return Journal().Wash;
        }

        public long CurrentInsertCoin()
        {
            return Journal().InsertCoin;
        }

        public long CurrentRefundCoin()
        {
            return Journal().RefundCoin;
        }

        public long CurrentProfit()
        {
            return Journal().Open - Journal().Wash;
        }

        public long CurrentCoinProfit()
        {
            return PreviousCoinProfit() + (CurrentProfit() - PreviousProfit()) / 1000;
        }

        public long CurrentGameBeat()
        {
            return Journal().Beat;
        }

        public long CurrentProfitTotal()
        {
            return CurrentProfit() + PreviousProfit();
        }

        public long CurrentProfitPrevious()
        {
            throw new NotImplementedException();
        }

        public long CurrentProfitCurrent()
        {
            throw new NotImplementedException();
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

        public long PreviousProfit()
        {
            return PreviousJournalArchives().Open - PreviousJournalArchives().Wash;
        }

        public long PreviousCoinProfit()
        {
            return 0;
        }

        public long PreviousGameBeat()
        {
            return 0;
        }

        public long PreviousProfitTotal()
        {
            return 0;
        }

        public long PreviousProfitPrevious()
        {
            return 0;
        }

        public long PreviousProfitCurrent()
        {
            return 0;
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

        public long AllProfit()
        {
            return 1;
        }

        public long AllPreviousProfit()
        {
            return 1;
        }

        public long AllOpen()
        {
            return 1;
        }

        public long AllWash()
        {
            return 1;
        }

        public long AllInsertCoin()
        {
            return 1;
        }

        public long AllRefundCoin()
        {
            return 1;
        }

        public long AllSpend()
        {
            return 1;
        }

        public long AllGain()
        {
            return 1;
        }

        public long AllThousandths()
        {
            return 1;
        }

        public long AllBeat()
        {
            return 1;
        }

        #endregion
    }
}