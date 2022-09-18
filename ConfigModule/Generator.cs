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
                Variable = DefaultVariable()
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
                Password = "fcdb4b423f4e5283afa249d762ef6aef150e91fccd810d43e5e719d14512dec7" //0 x16
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
                Password = "7e071fd9b023ed8f18458a73613a0834f6220bd5cc50357ba3493c6040a9ea8c" //00000000
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
    }
}