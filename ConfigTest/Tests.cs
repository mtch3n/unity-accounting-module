using System;
using System.IO;
using FisherConfig;
using FisherConfig.Utils;
using Newtonsoft.Json;
using NUnit.Framework;

namespace ConfigTest
{
    [TestFixture]
    public class Tests
    {
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
            var loader = new Loader("/home/chenmt/tmp/config.json");

            loader.Load();
            var cfg = loader.GetConfig();

            var json = JsonConvert.SerializeObject(cfg, Formatting.Indented);
            Console.Write(json);

            Assert.True(true);
        }

        [Test]
        public void TestLoadConfigFileNotFound()
        {
            var loader = new Loader("/home/chenmt/tmp/filenotfound.json");
            Assert.Throws<FileNotFoundException>(
                () => { loader.Load(); });
        }

        [Test]
        public void TestConfigSave()
        {
            var loader = new Loader("/home/chenmt/tmp/config.json");
            loader.Load();

            var cfg = loader.GetConfig();

            var assertValue = 100;
            cfg.Variable.MinBet = assertValue;
            loader.Save();

            //reload
            var loaderNew = new Loader("/home/chenmt/tmp/config.json");
            loader.Load();

            var cfgNew = loader.GetConfig();

            Assert.AreEqual(assertValue, cfgNew.Variable.MinBet);
        }

        [Test]
        public void TestPassword()
        {
            var loader = new Loader("/home/chenmt/tmp/config.json");
            loader.Load();
            Assert.True(loader.Valid("0000000000000000"));
        }

        [Test]
        public void TestWrongPassword()
        {
            var loader = new Loader("/home/chenmt/tmp/config.json");
            loader.Load();
            Assert.True(!loader.Valid("wrong"));
        }

        [Test]
        public void TestRemotePassword()
        {
            var loader = new Loader("/home/chenmt/tmp/config.json");
            loader.Load();
            Assert.True(loader.RemoteValid("00000000"));
        }

        [Test]
        public void TestWrongRemotePassword()
        {
            var loader = new Loader("/home/chenmt/tmp/config.json");
            loader.Load();
            Assert.True(!loader.RemoteValid("wrong"));
        }

        [Test]
        public void TestNewPassword()
        {
            var assert = "test";

            var loader = new Loader("/home/chenmt/tmp/config.json");
            loader.Load();
            loader.NewMachinePassword(assert);
            var c = loader.GetConfig();

            Assert.AreEqual(Hash.GetSha256String(assert), c.Info.Password);
        }

        [Test]
        public void TestNewRemotePassword()
        {
            var assert = "test";

            var loader = new Loader("/home/chenmt/tmp/config.json");
            loader.Load();
            loader.NewRemotePassword(assert);
            var c = loader.GetConfig();

            Assert.AreEqual(Hash.GetSha256String(assert), c.Remote.Password);
        }
    }
}