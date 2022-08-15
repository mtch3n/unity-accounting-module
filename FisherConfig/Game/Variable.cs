namespace FisherConfig.Game
{
    public class Variable
    {
        public int MinBet { get; set; }

        public int MaxBet { get; set; }

        public int PointInterval { get; set; }

        public int LotteryRatio { get; set; }

        public PlayMode GameMode { get; set; }

        public BulletSpeed BulletSpeed { get; set; }

        public AutoFireSpeed AutoFireSpeed { get; set; }

        public bool EnableAutoFire { get; set; }

        public bool Lock { get; set; }

        public bool InvertTurret { get; set; }

        public int Players { get; set; }

        public VolumeMode BackgroundVolume { get; set; }

        public int Volume { get; set; }
    }

    public enum AutoFireSpeed
    {
        Fast,
        Normal,
        Slow,
        Disable,
    }

    public enum BulletSpeed
    {
        Fast,
        Normal,
        Slow,
    }

    public enum VolumeMode
    {
        PlayAll,
        MuteAll,
        FxOnly,
        MusicOnly,
    }
}