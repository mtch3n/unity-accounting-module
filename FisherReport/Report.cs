using System;
using FisherReport.Data;

namespace FisherReport
{
    public class Report
    {
        private readonly ReportLog _memReport = new ReportLog();

        Report()
        {
            // if (Instance.TimeStamp == 0)
            // {
            // }
        }

        public void LogOpen(int value)
        {
            _memReport.Open += value;
        }

        public void LogWash(int value)
        {
            _memReport.Wash += value;
        }

        public void LogInsertCoin(int value)
        {
            _memReport.InsertCoin += value;
        }

        public void LogRefundCoin(int value)
        {
            _memReport.RefundCoin += value;
        }

        public void LogPointGain(int value)
        {
            _memReport.PointGain += value;
        }

        public void LogPointSpend(int value)
        {
            _memReport.PointSpend += value;
        }

        public ReportLog Log()
        {
            return _memReport;
        }

        private void AppendLog(int value)
        {
        }
    }
}