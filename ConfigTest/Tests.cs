using System;
using NUnit.Framework;
using FisherConfig;
using Newtonsoft.Json;

namespace ConfigTest
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void TestLoadNewConfig()
        {
            // var c = new Loader("/home/chenmt/tmp/game.json");
            var a = new Generator();

            var c = a.DefaultConfig();

            var json = JsonConvert.SerializeObject(c, Formatting.Indented);
            Console.Write(json);

            // a.Variable.Lock = true;
            // a.Info.Password;
            Assert.True(true);
        }
    }
}