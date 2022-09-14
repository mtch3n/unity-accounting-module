using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace AccountingModule.Data
{
    [Serializable]
    public class Ledger
    {
        public long Open { get; set; }

        public long Wash { get; set; }

        public long InsertCoin { get; set; }

        public long RefundCoin { get; set; }

        public long PointGain { get; set; }

        public long PointSpend { get; set; }

        public long TimeStamp { get; set; }

        public long Profit()
        {
            return Open - Wash;
        }

        public byte[] Serialize()
        {
            byte[] bytes;
            IFormatter formatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, this);
                bytes = stream.ToArray();
            }

            return bytes;
        }
    }

    public enum ReportType
    {
        Open,
        Wash,
        InsertCoin,
        RefundCoin,
        PointGain,
        PointSpend
    }
}