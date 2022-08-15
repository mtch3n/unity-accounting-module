using System;

namespace FisherConfig.Game
{
    public class Remote
    {
        public int CoinRatio { get; set; }

        public int OpenRatio { get; set; }

        public int Yxnd { get; set; }

        public Yxlx Yxlx { get; set; }

        public Fbcx Fbcx { get; set; }

        public long ScoreOpenMax { get; set; }

        public long BeatingLimit { get; set; }

        public int AccountingTime { get; set; }

        public int Password { get; set; }
    }

    public enum Yxlx
    {
        Big,
        Medium,
        Small
    };

    public enum Fbcx
    {
        High,
        Medium,
        Low
    };
}