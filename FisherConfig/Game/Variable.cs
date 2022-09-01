namespace FisherConfig.Game
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
        S0,
        S1,
        S2,
        S3,
        S4,
        S5,
        S6,
        S7,
        S8
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