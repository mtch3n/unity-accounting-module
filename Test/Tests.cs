using System;
using System.IO;
using ConfigModule;
using ConfigModule.Utils;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Test
{
    [TestFixture]
    public class Tests
    {
        private Option GetOption()
        {
            return new Option
            {
                Path = "/home/chenmt/tmp/config.json",
            };
        }

        [Test]
        public void TestDefaultConfig()
        {
            var a = new Generator();

            var c = a.DefaultConfig();

            var json = JsonConvert.SerializeObject(c, Formatting.Indented);
            Console.Write(json);

            Assert.True(true);
        }

        [Test]
        public void TestLoadConfig()
        {
            var loader = new Loader(GetOption());

            loader.Load();
            var cfg = loader.GetConfig();

            var json = JsonConvert.SerializeObject(cfg, Formatting.Indented);
            Console.Write(json);

            Assert.True(true);
        }

        [Test]
        public void TestLoadConfigFileNotFound()
        {
            var loader = new Loader(new Option { Path = "/home/chenmt/tmp/notfound.config" });
            Assert.Throws<FileNotFoundException>(
                () => { loader.Load(); });
        }

        [Test]
        public void TestConfigSave()
        {
            var loader = new Loader(GetOption());
            loader.Load();

            var cfg = loader.GetConfig();

            var assertValue = 100;
            cfg.Variable.MinBet = assertValue;
            loader.Save();

            //reload
            var loaderNew = new Loader(GetOption());
            loader.Load();

            var cfgNew = loaderNew.GetConfig();

            Assert.AreEqual(assertValue, cfgNew.Variable.MinBet);
        }

        [Test]
        public void TestPassword()
        {
            var loader = new Loader(GetOption());
            loader.Load();
            Assert.True(loader.Valid("0000000000000000"));
        }

        [Test]
        public void TestWrongPassword()
        {
            var loader = new Loader(GetOption());
            loader.Load();
            Assert.True(!loader.Valid("wrong"));
        }

        [Test]
        public void TestRemotePassword()
        {
            var loader = new Loader(GetOption());
            loader.Load();
            Assert.True(loader.RemoteValid("00000000"));
        }

        [Test]
        public void TestWrongRemotePassword()
        {
            var loader = new Loader(GetOption());
            loader.Load();
            Assert.True(!loader.RemoteValid("wrong"));
        }

        [Test]
        public void TestNewPassword()
        {
            var assert = "test";

            var loader = new Loader(GetOption());
            loader.Load();
            loader.NewMachinePassword(assert);
            var c = loader.GetConfig();

            Assert.AreEqual(Hash.GetSha256String(assert), c.Info.Password);
        }

        [Test]
        public void TestNewRemotePassword()
        {
            var assert = "test";

            var loader = new Loader(GetOption());
            loader.Load();
            loader.NewRemotePassword(assert);
            var c = loader.GetConfig();

            Assert.AreEqual(Hash.GetSha256String(assert), c.Remote.Password);
        }
    }
}