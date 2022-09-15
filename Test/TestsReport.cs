using System.IO;
using System.Linq;
using AccountingModule;
using NUnit.Framework;

namespace Test
{
    [TestFixture]
    public class TestsAccounting
    {
        [Test]
        public void TestAccounting()
        {
            File.Delete("/home/chenmt/tmp/wal.bin");
            File.Delete("/home/chenmt/tmp/data.bin");

            var opt = new Option
            {
                Path = "/home/chenmt/tmp/"
            };

            var rp = new Report(opt);

            rp = null;
            rp = new Report(opt);

            var rec = rp.Journal();
            Assert.AreEqual(0, rec.Open);
            Assert.AreEqual(0, rec.Wash);
            Assert.AreEqual(0, rec.InsertCoin);
            Assert.AreEqual(0, rec.PointGain);
            Assert.AreEqual(0, rec.PointSpend);
            Assert.AreEqual(0, rec.RefundCoin);
        }

        [Test]
        public void TestAccountingResume()
        {
            File.Delete("/home/chenmt/tmp/wal.bin");
            File.Delete("/home/chenmt/tmp/data.bin");

            var opt = new Option
            {
                Path = "/home/chenmt/tmp/"
            };

            var rp = new Report(opt);

            rp.LogOpen(1000);

            rp = null;
            rp = new Report(opt);

            var rec = rp.Journal();
            Assert.AreEqual(1000, rec.Open);
            Assert.AreEqual(0, rec.Wash);
            Assert.AreEqual(0, rec.InsertCoin);
            Assert.AreEqual(0, rec.PointGain);
            Assert.AreEqual(0, rec.PointSpend);
            Assert.AreEqual(0, rec.RefundCoin);
        }

        [Test]
        public void TestAccountingCommitResume()
        {
            File.Delete("/home/chenmt/tmp/wal.bin");
            File.Delete("/home/chenmt/tmp/data.bin");

            var opt = new Option
            {
                Path = "/home/chenmt/tmp/",
                CommitThreshold = 10
            };

            var rp = new Report(opt);
            for (var i = 0; i < 11; i++) rp.LogOpen(1);

            Assert.AreEqual(11, rp.Journal().Open);

            rp = null;
            rp = new Report(opt);

            var rec = rp.Journal();
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
            File.Delete("/home/chenmt/tmp/wal.bin");
            File.Delete("/home/chenmt/tmp/data.bin");

            var opt = new Option
            {
                Path = "/home/chenmt/tmp/",
                CommitThreshold = 10000,
                // MemWal = true,
                NoCommit = true
            };

            var rp = new Report(opt);
            for (var i = 0; i < 9999; i++)
            {
                rp.LogOpen(1);
                rp.LogWash(1);
                rp.LogInsertCoin(1);
                rp.LogRefundCoin(1);
            }

            Assert.True(true);
        }


        [Test]
        public void TestReset()
        {
            File.Delete("/home/chenmt/tmp/wal.bin");
            File.Delete("/home/chenmt/tmp/data.bin");

            var opt = new Option
            {
                Path = "/home/chenmt/tmp/",
                CommitThreshold = 10000
            };

            var rp = new Report(opt);
            rp.LogOpen(1);
            rp.LogWash(1);
            rp.LogInsertCoin(1);
            rp.LogRefundCoin(1);
            rp.LogPointGain(1);
            rp.LogPointSpend(1);

            var rec = rp.Journal();
            Assert.AreEqual(1, rec.Open);
            Assert.AreEqual(1, rec.Wash);
            Assert.AreEqual(1, rec.InsertCoin);
            Assert.AreEqual(1, rec.PointGain);
            Assert.AreEqual(1, rec.PointSpend);
            Assert.AreEqual(1, rec.RefundCoin);

            rp.Reset();
            rec = rp.Journal();

            Assert.AreEqual(0, rec.Open);
            Assert.AreEqual(0, rec.Wash);
            Assert.AreEqual(0, rec.InsertCoin);
            Assert.AreEqual(0, rec.PointGain);
            Assert.AreEqual(0, rec.PointSpend);
            Assert.AreEqual(0, rec.RefundCoin);
        }

        [Test]
        public void TestArchive()
        {
            File.Delete("/home/chenmt/tmp/wal.bin");
            File.Delete("/home/chenmt/tmp/archive.bin");
            File.Delete("/home/chenmt/tmp/data.bin");

            var opt = new Option
            {
                Path = "/home/chenmt/tmp/",
                CommitThreshold = 10000
            };

            var rp = new Report(opt);
            rp.LogOpen(1);
            rp.LogWash(1);
            rp.LogInsertCoin(1);
            rp.LogRefundCoin(1);
            rp.LogPointGain(1);
            rp.LogPointSpend(1);

            var rec = rp.Journal();
            Assert.AreEqual(1, rec.Open);
            Assert.AreEqual(1, rec.Wash);
            Assert.AreEqual(1, rec.InsertCoin);
            Assert.AreEqual(1, rec.PointGain);
            Assert.AreEqual(1, rec.PointSpend);
            Assert.AreEqual(1, rec.RefundCoin);

            rp.DoArchive();

            var arc = rp.Archive();
            Assert.AreEqual(1, arc.JournalArchives.Count);

            var d = arc.JournalArchives.First();
            Assert.AreEqual(1, d.Open);
            Assert.AreEqual(1, d.Wash);
            Assert.AreEqual(1, d.InsertCoin);
            Assert.AreEqual(1, d.PointGain);
            Assert.AreEqual(1, d.PointSpend);
            Assert.AreEqual(1, d.RefundCoin);
        }

        [Test]
        public void TestArchive2()
        {
            File.Delete("/home/chenmt/tmp/wal.bin");
            File.Delete("/home/chenmt/tmp/archive.bin");
            File.Delete("/home/chenmt/tmp/data.bin");

            var opt = new Option
            {
                Path = "/home/chenmt/tmp/",
                CommitThreshold = 10000
            };

            var rp = new Report(opt);
            for (var i = 0; i < 2; i++)
            {
                rp.LogOpen(1);
                rp.LogWash(1);
                rp.LogInsertCoin(1);
                rp.LogRefundCoin(1);
                rp.LogPointGain(1);
                rp.LogPointSpend(1);

                var rec = rp.Journal();
                Assert.AreEqual(1, rec.Open);
                Assert.AreEqual(1, rec.Wash);
                Assert.AreEqual(1, rec.InsertCoin);
                Assert.AreEqual(1, rec.PointGain);
                Assert.AreEqual(1, rec.PointSpend);
                Assert.AreEqual(1, rec.RefundCoin);

                rp.DoArchive();
            }

            var arc = rp.Archive();

            Assert.AreEqual(2, arc.JournalArchives.Count);

            var d = arc.JournalArchives.First();
            Assert.AreEqual(1, d.Open);
            Assert.AreEqual(1, d.Wash);
            Assert.AreEqual(1, d.InsertCoin);
            Assert.AreEqual(1, d.PointGain);
            Assert.AreEqual(1, d.PointSpend);
            Assert.AreEqual(1, d.RefundCoin);
        }

        [Test]
        public void TestQueryBuilder()
        {
            File.Delete("/home/chenmt/tmp/wal.bin");
            File.Delete("/home/chenmt/tmp/archive.bin");
            File.Delete("/home/chenmt/tmp/data.bin");

            var opt = new Option
            {
                Path = "/home/chenmt/tmp/",
                CommitThreshold = 10000
            };

            var rp = new Report(opt);
        }
    }
}