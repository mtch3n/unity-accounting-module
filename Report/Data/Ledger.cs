namespace Report.Data
{
    public class Ledger
    {
        public long Open { get; set; }

        public long Wash { get; set; }

        public long InsertCoin { get; set; }

        public long RefundCoin { get; set; }

        public long PointGain { get; set; }

        public long PointSpend { get; set; }

        public long TimeStamp { get; set; }
    }

    public enum ReportType
    {
        Open,
        Wash,
        InsertCoin,
        RefundCoin,
        PointGain,
        PointSpend
    }
}