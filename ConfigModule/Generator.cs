using ConfigModule.Game;
using ConfigModule.Machine;

namespace ConfigModule
{
    public class Generator
    {
        public Config DefaultConfig()
        {
            var conf = new Config
            {
                Info = DefaultInfo(),
                Remote = DefaultRemote(),
                Variable = DefaultVariable(),
                BillReader = DefaultBillReader()
            };

            return conf;
        }


        public Info DefaultInfo()
        {
            return new Info
            {
                AccountingCount = 0,
                TypeNo = "1234",
                MachineNo = "12345678",
                ConfirmCode = "9876543210test",
                Password = "33cdbc3872b3789776eff6178cd7585d9c9b080c752aa4e92c274d768e2a7ea2" //1234567812345678
            };
        }

        public Remote DefaultRemote()
        {
            return new Remote
            {
                CoinRatio = 0,
                OpenRatio = 10000,
                Yxnd = Yxnd.Mid,
                Yxlx = Yxlx.Medium,
                Fbcx = Fbcx.Medium,
                ScoreOpenMax = 500000,
                BeatingLimit = 3000000,
                AccountingTime = 30, //day
                Password = "ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f" //12345678
            };
        }

        public Variable DefaultVariable()
        {
            return new Variable
            {
                MinBet = 10,
                MaxBet = 1000,
                PointInterval = 10,
                FireSpeed = FireSpeed.Normal,
                BulletSpeed = BulletSpeed.Normal,
                TimedFiring = TimedFiring.Disable,
                EnableAutoFire = false,
                Lock = true,
                InvertTurret = false,
                Players = Players.P6,
                BackgroundVolume = VolumeMode.PlayAll,
                Volume = VolumeStep.S4,
                GameMode = PlayMode.ButtonRefundCoin,
                LotteryRatio = LotteryRatio.I1
            };
        }

        public BillReader DefaultBillReader()
        {
            return new BillReader
            {
                Reader1 = 0,
                Reader2 = 0,
                Reader3 = 0,
                Reader4 = 0,
                Reader5 = 0,
                Reader6 = 0,
                Reader7 = 0,
                Reader8 = 0,
                Reader9 = 0,
                Reader10 = 0,
                Reader11 = 0,
                Reader12 = 0,
                Reader13 = 0,
                Reader14 = 0,
                Reader15 = 0,
                Reader16 = 0
            };
        }
    }
}