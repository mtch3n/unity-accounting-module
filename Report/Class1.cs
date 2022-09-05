namespace Report
{
    public class Report
    {
        public void LogOpen(int value)
        {
            _memReport.Open += value;
        }

        public void LogWash(int value)
        {
            _memReport.Wash += value;
        }

        public void LogInsertCoin(int value)
        {
            _memReport.InsertCoin += value;
        }

        public void LogRefundCoin(int value)
        {
            _memReport.RefundCoin += value;
        }

        public void LogPointGain(int value)
        {
            _memReport.PointGain += value;
        }

        public void LogPointSpend(int value)
        {
            _memReport.PointSpend += value;
        }
    }
}