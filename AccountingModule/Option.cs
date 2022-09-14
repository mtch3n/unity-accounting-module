namespace AccountingModule
{
    public class Option
    {
        public Option()
        {
            CommitThreshold = 100;
        }

        public string Path { get; set; }
        public int CommitThreshold { get; set; }
        public bool DiscardLogs { get; set; }
        public bool NoCommit { get; set; }

        public bool MemWal { get; set; }
    }
}