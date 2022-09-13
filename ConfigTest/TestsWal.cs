﻿using NUnit.Framework;
using Report;
using Report.Data;

namespace ConfigTest
{
    [TestFixture]
    public class TestsWal
    {
        [Test]
        public void TestReport()
        {
            var opt = new Option
            {
                Path = "/home/chenmt/tmp/"
            };

            var wal = new WAL(opt);

            for (var i = 0; i < 3; i++)
            {
                wal.Append(new ReportLog
                {
                    Type = ReportType.Open, Value = 1
                });
            }

            Assert.AreEqual(3, wal.Count());

            wal.Flush();
        }

        [Test]
        public void TestLoad()
        {
            var wal = new WAL(new Option
            {
                Path = "/home/chenmt/tmp/"
            });
            
            wal.Flush();

            for (var i = 0; i < 3; i++)
            {
                wal.Append(new ReportLog
                {
                    Type = ReportType.Open, Value = 1
                });
            }

            wal = null;

            wal = new WAL(new Option
            {
                Path = "/home/chenmt/tmp/"
            });

            wal.Open();

            Assert.AreEqual(3, wal.Count());
        }
    }
}