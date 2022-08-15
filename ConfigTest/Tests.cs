using System;
using NUnit.Framework;
using FisherConfig;

namespace ConfigTest
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void TestLoadNewConfig()
        {
            var a = Loader.NewCfg();
            a.Info.Password;
            Assert.True(true);
        }
    }
}