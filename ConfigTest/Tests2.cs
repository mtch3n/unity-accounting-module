using NUnit.Framework;
using Report;
using Report.Data;

namespace ConfigTest
{
    [TestFixture]
    public class Tests2
    {
        [Test]
        public void TestReport()
        {
            // var opt = new Option
            // {
            //     Path = "/home/chenmt/tmp/"
            // };
            //
            // var wal = new WAL(opt);
            //
            // for (var i = 0; i < 3; i++)
            // {
            //     wal.Append(new ReportLog
            //     {
            //         Type = ReportType.Open, Value = 1
            //     });
            // }

            Assert.IsTrue(true);
        }
        
        [Test]
        public void TestLoad()
        {
            var opt = new Option
            {
                Path = "/home/chenmt/tmp/"
            };
            
            var wal = new WAL(opt);
            
            wal.Open();
            
            wal.Read(0);

            Assert.IsTrue(true);
        }
    }
}