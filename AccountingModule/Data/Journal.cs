using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace AccountingModule.Data
{
    [Serializable]
    public class Journal
    {
        public Dictionary<PlayerBet, long> PlayerBet = new Dictionary<PlayerBet, long>();
        public Dictionary<PlayerScore, long> PlayerScore = new Dictionary<PlayerScore, long>();
        public long Open { get; set; }

        public long Wash { get; set; }

        public long InsertCoin { get; set; }

        public long RefundCoin { get; set; }

        public long PointGain { get; set; }

        public long PointSpend { get; set; }

        public long Profit { get; set; }

        public long ProfitPoint => Open - Wash;

        public long ProfitCoin { get; set; }

        public long TimeStamp { get; set; }
        public long Beat { get; set; }

        public void CalculateProfit(long lastProfit, long lastGain, long lastSpent)
        {
            Profit = lastProfit + (PointGain - lastGain - (PointSpend - lastSpent)) / 1000;
        }

        public void CalculateProfitCoin(long lastProfitCoin, long lastProfitPoint)
        {
            ProfitCoin = lastProfitCoin + (ProfitPoint - lastProfitPoint) / 1000;
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
}