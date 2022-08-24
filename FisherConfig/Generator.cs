using FisherConfig.Game;
using FisherConfig.Machine;

namespace FisherConfig
{
    public class Generator
    {
        private Config NewConfigContainer()
        {
            return new Config();
        }

        public Config DefaultConfig()
        {
            var conf = new Config
            {
                Info = CreateDefaultInfo(),
                Remote = CreateDefaultRemote(),
                Variable = CreateDefaultVariable()
            };

            return conf;
        }


        private Info CreateDefaultInfo()
        {
            return new Info
            {
                AccountingCount = 0,
                TypeNo = "test",
                MachineNo = "test0123456789",
                ConfirmCode = "9876543210test",
                Password = "fcdb4b423f4e5283afa249d762ef6aef150e91fccd810d43e5e719d14512dec7" //0 x16
            };
        }

        private Remote CreateDefaultRemote()
        {
            return new Remote
            {
                CoinRatio = 0,
                OpenRatio = 10000,
                Yxnd = Yxnd.Nd3,
                Yxlx = Yxlx.Medium,
                Fbcx = Fbcx.Medium,
                ScoreOpenMax = 500000,
                BeatingLimit = 3000000,
                AccountingTime = 30,
                Password = "7e071fd9b023ed8f18458a73613a0834f6220bd5cc50357ba3493c6040a9ea8c" //00000000
            };
        }

        private Variable CreateDefaultVariable()
        {
            return new Variable
            {
                MinBet = 10,
                MaxBet = 9999,
                PointInterval = 1000,
                BulletSpeed = BulletSpeed.Normal,
                TimedFiring = TimedFiring.Disable,
                EnableAutoFire = false,
                Lock = true,
                InvertTurret = false,
                Players = Players.P6,
                BackgroundVolume = VolumeMode.PlayAll,
                Volume = VolumeStep.S4,
                GameMode = CreateDefaultPlayMode(),
                LotteryRatio = LotteryRatio.F1
            };
        }

        private PlayMode CreateDefaultPlayMode()
        {
            return new PlayMode();
        }
    }
}