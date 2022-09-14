using System;
using System.IO;
using NUnit.Framework;
using Report;
using Report.Data;

namespace ConfigTest
{
    [TestFixture]
    public class TestsReport
    {
        [Test]
        public void TestReport()
        {
            File.Delete("/home/chenmt/tmp/wal.dat");
            File.Delete("/home/chenmt/tmp/commit.dat");

            var opt = new Option
            {
                Path = "/home/chenmt/tmp/"
            };

            var rp = new Report.Report(opt);

            rp = null;
            rp = new Report.Report(opt);

            var rec = rp.GetLedger();
            Assert.AreEqual(0, rec.Open);
            Assert.AreEqual(0, rec.Wash);
            Assert.AreEqual(0, rec.InsertCoin);
            Assert.AreEqual(0, rec.PointGain);
            Assert.AreEqual(0, rec.PointSpend);
            Assert.AreEqual(0, rec.RefundCoin);
        }

        [Test]
        public void TestReportResume()
        {
            File.Delete("/home/chenmt/tmp/wal.dat");
            File.Delete("/home/chenmt/tmp/commit.dat");

            var opt = new Option
            {
                Path = "/home/chenmt/tmp/"
            };

            var rp = new Report.Report(opt);

            rp.LogOpen(1000);

            rp = null;
            rp = new Report.Report(opt);

            var rec = rp.GetLedger();
            Assert.AreEqual(1000, rec.Open);
            Assert.AreEqual(0, rec.Wash);
            Assert.AreEqual(0, rec.InsertCoin);
            Assert.AreEqual(0, rec.PointGain);
            Assert.AreEqual(0, rec.PointSpend);
            Assert.AreEqual(0, rec.RefundCoin);
        }

        [Test]
        public void TestReportCommitResume()
        {
            File.Delete("/home/chenmt/tmp/wal.dat");
            File.Delete("/home/chenmt/tmp/commit.dat");

            var opt = new Option
            {
                Path = "/home/chenmt/tmp/",
                CommitThreshold = 10,
            };

            var rp = new Report.Report(opt);
            for (int i = 0; i < 11; i++)
            {
                rp.LogOpen(1);
            }

            Assert.AreEqual(11, rp.GetLedger().Open);

            rp = null;
            rp = new Report.Report(opt);

            var rec = rp.GetLedger();
            Assert.AreEqual(11, rec.Open);
            Assert.AreEqual(0, rec.Wash);
            Assert.AreEqual(0, rec.InsertCoin);
            Assert.AreEqual(0, rec.PointGain);
            Assert.AreEqual(0, rec.PointSpend);
            Assert.AreEqual(0, rec.RefundCoin);
        }

        [Test]
        public void TestLargeWrite()
        {
            File.Delete("/home/chenmt/tmp/wal.dat");
            File.Delete("/home/chenmt/tmp/commit.dat");

            var opt = new Option
            {
                Path = "/home/chenmt/tmp/",
                CommitThreshold = 10000,
                // MemWal = true,
                NoCommit = true
            };

            var rp = new Report.Report(opt);
            for (int i = 0; i < 9999; i++)
            {
                rp.LogOpen(1);
                rp.LogWash(1);
                rp.LogInsertCoin(1);
                rp.LogRefundCoin(1);
            }

            Assert.True(true);
        }
    }
}