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
                    return "大追殺";
                case Yxnd.MidHigh:
                    return "追殺";
                case Yxnd.Mid:
                    return "正常";
                case Yxnd.MidLow:
                    return "放水";
                case Yxnd.Low:
                    return "大放水";
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
        Big = 30,
        Medium = 10,
        Small = 0
    }

    public enum Fbcx
    {
        High = 20,
        Medium = 10,
        Low = 5
    }
}