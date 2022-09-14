namespace ConfigModule.Game
{
    public class Remote
    {
        public int CoinRatio { get; set; }

        public int OpenRatio { get; set; }

        public Yxnd Yxnd { get; set; }

        public Yxlx Yxlx { get; set; }

        public Fbcx Fbcx { get; set; }

        public long ScoreOpenMax { get; set; }

        public long BeatingLimit { get; set; }

        public int AccountingTime { get; set; }

        public string Password { get; set; }
    }

    public enum Yxnd
    {
        Low = 120,
        MidLow = 110,
        Mid = 97,
        MidHigh = 80,
        High = 70
    }

    public enum Yxlx
    {
        Big,
        Medium,
        Small
    }

    public enum Fbcx
    {
        High,
        Medium,
        Low
    }
}