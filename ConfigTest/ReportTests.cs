using System;
using System.IO;
using FisherConfig;
using FisherConfig.Utils;
using Newtonsoft.Json;
using NUnit.Framework;

namespace ConfigTest
{
    [TestFixture]
    public class ReportTests
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
    }
}