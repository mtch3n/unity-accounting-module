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

        public string YxndString()
        {
            switch (Yxnd)
            {
                case Yxnd.High:
                    return "1";
                case Yxnd.MidHigh:
                    return "2";
                case Yxnd.Mid:
                    return "3";
                case Yxnd.MidLow:
                    return "4";
                case Yxnd.Low:
                    return "5";
                default:
                    return null;
            }
        }

        public string YxlxString()
        {
            switch (Yxlx)
            {
                case Yxlx.Big:
                    return "大";
                case Yxlx.Medium:
                    return "中";
                case Yxlx.Small:
                    return "小";
                default: return null;
            }
        }

        public string FbcxString()
        {
            switch (Fbcx)
            {
                case Fbcx.High:
                    return "高";
                case Fbcx.Medium:
                    return "中";
                case Fbcx.Low:
                    return "低";
                default: return null;
            }
        }
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