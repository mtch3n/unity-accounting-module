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
            return 0;
        }

        public long CurrentWash()
        {
            return 0;
        }

        public long CurrentInsertCoin()
        {
            return 0;
        }

        public long CurrentRefundCoin()
        {
            return 0;
        }

        public long CurrentProfit()
        {
            return 0;
        }

        public long CurrentCoinProfit()
        {
            return 0;
        }

        public long CurrentGameBeat()
        {
            return 0;
        }

        public long CurrentProfitTotal()
        {
            return 0;
        }

        public long CurrentProfitPrevious()
        {
            return 0;
        }

        public long CurrentProfitCurrent()
        {
            return 0;
        }

        #endregion

        #region previous

        public long PreviousOpen()
        {
            return 0;
        }

        public long PreviousWash()
        {
            return 0;
        }

        public long PreviousInsertCoin()
        {
            return 0;
        }

        public long PreviousRefundCoin()
        {
            return 0;
        }

        public long PreviousProfit()
        {
            return 0;
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

        public void ReportCount()
        {
        }

        #endregion

        #region

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