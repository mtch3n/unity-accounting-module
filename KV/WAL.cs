namespace KV
{
    public class WAL
    {
        private long entryIndex;
        private byte[] data;
        private long timeStamp;

        public void WriteEntry(long entryIndex, byte[] data, long timeStamp)
        {
            this.entryIndex = entryIndex;
            this.data = data;
            this.timeStamp = timeStamp;
        }
    }
}