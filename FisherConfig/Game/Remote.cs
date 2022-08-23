namespace FisherConfig.Game
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
        Nd0,
        Nd1,
        Nd2,
        Nd3,
        Nd4,
        Nd5,
        Nd6,
        Nd7,
        Nd8,
        Nd9,
        Nd010,
        Nd11,
        Nd12,
        Nd13,
        Nd14,
        Nd15,
        Nd16,
        Nd17,
        Nd18,
        Nd19,
        Nd20
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