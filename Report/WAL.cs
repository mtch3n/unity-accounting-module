using System.Collections.Generic;
using Report.Data;

namespace Report
{
    public class WAL
    {
        private const int WalLimit = 1000;

        private List<LogEntry> entries = new List<LogEntry>();
        private int index;

        private int fileWriter;

        public void Append(ReportType type, int value)
        {
            var seg = new LogEntry()
            {
                index = index,
                type = type,
                value = value
            };

            Write(seg);

            index++;
        }

        private void Write(LogEntry entry)
        {
            entries.Clear();
            entries.Add(entry);
        }
    }
}