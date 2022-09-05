using NUnit.Framework;

namespace ConfigTest
{
    [TestFixture]
    public class Tests2
    {
        [Test]
        public void TestReport()
        {
            var a = new Class1();

            a.Test();

            Assert.IsTrue(true);
        }
    }
}