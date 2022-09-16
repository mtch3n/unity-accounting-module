namespace ConfigModule.Game
{
    public class Variable
    {
        public int MinBet { get; set; }

        public int MaxBet { get; set; }

        public int PointInterval { get; set; }

        public LotteryRatio LotteryRatio { get; set; }

        public PlayMode GameMode { get; set; }

        public FireSpeed FireSpeed { get; set; }

        public BulletSpeed BulletSpeed { get; set; }

        public TimedFiring TimedFiring { get; set; }

        public bool EnableAutoFire { get; set; }

        public bool Lock { get; set; }

        public bool InvertTurret { get; set; }

        public Players Players { get; set; }

        public VolumeMode BackgroundVolume { get; set; }

        public VolumeStep Volume { get; set; }

        public string LotteryRatioString()
        {
            if (LotteryRatio.ToString().Contains("I")) return LotteryRatio.ToString().Replace("I", "");

            if (LotteryRatio.ToString().Contains("F")) return LotteryRatio.ToString().Replace("F", "1/");

            return null;
        }

        public string PlayModeString()
        {
            if (GameMode.ButtonRefundCoin) return "按鈕退幣";
            if (GameMode.ButtonRefundLottery) return "按鈕退彩票";
            if (GameMode.RefundCoinInGame) return "遊戲中退幣";
            if (GameMode.RefundLotteryInGame) return "遊戲中退彩票";
            if (GameMode.ComType) return "通訊模式";
            if (GameMode.PrintLotteryOnPress) return "按鍵印票";

            return null;
        }

        public string FireSpeedString()
        {
            switch (FireSpeed)
            {
                case FireSpeed.Slow:
                    return "慢";
                case FireSpeed.Normal:
                    return "中";
                case FireSpeed.Fast:
                    return "快";
                default:
                    return null;
            }
        }

        public string BulletSpeedString()
        {
            switch (BulletSpeed)
            {
                case BulletSpeed.Slow:
                    return "慢";
                case BulletSpeed.Normal:
                    return "中";
                case BulletSpeed.Fast:
                    return "快";
                default:
                    return null;
            }
        }

        public string TimedFiringString()
        {
            switch (TimedFiring)
            {
                case TimedFiring.Slow:
                    return "慢";
                case TimedFiring.Normal:
                    return "中";
                case TimedFiring.Fast:
                    return "快";
                default:
                    return null;
            }
        }

        public string EnableAutoFireString()
        {
            return EnableAutoFire ? "開" : "關";
        }

        public string LockString()
        {
            return Lock ? "開" : "關";
        }

        public string InvertTurretString()
        {
            return InvertTurret ? "正向" : "反向";
        }

        public string BackgroundVolumeString()
        {
            switch (BackgroundVolume)
            {
                case VolumeMode.PlayAll:
                    return "開啟機台全部聲音";
                case VolumeMode.MuteAll:
                    return "關閉機台全部聲音";
                case VolumeMode.FxOnly:
                    return "機台只播放音效";
                case VolumeMode.MusicOnly:
                    return "機台只播放音";
                default:
                    return null;
            }
        }

        public string VolumeStepString()
        {
            return ((int)Volume).ToString();
        }
    }

    public enum FireSpeed
    {
        Fast = 2,
        Normal = 3,
        Slow = 5
    }

    public enum TimedFiring
    {
        Fast = 3,
        Normal = 18,
        Slow = 48,
        Disable = 0
    }

    public enum BulletSpeed
    {
        Fast,
        Normal,
        Slow
    }

    public enum VolumeMode
    {
        PlayAll,
        MuteAll,
        FxOnly,
        MusicOnly
    }

    public enum Players
    {
        P6,
        P8,
        P10
    }

    public enum VolumeStep
    {
        S0 = 0,
        S1 = 1,
        S2 = 2,
        S3 = 3,
        S4 = 4,
        S5 = 5,
        S6 = 6,
        S7 = 7,
        S8 = 8
    }

    // I => integer     I10 => 10
    // F => Fraction    F10 => 1/10
    public enum LotteryRatio
    {
        I1,
        I2,
        I3,
        I4,
        I5,
        I8,
        I10,
        I15,
        I20,
        I25,
        I50,
        I80,
        I100,
        F1,
        F2,
        F3,
        F4,
        F5,
        F8,
        F10,
        F15,
        F20,
        F25,
        F50,
        F80,
        F100
    }
}