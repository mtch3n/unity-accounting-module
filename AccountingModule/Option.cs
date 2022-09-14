namespace AccountingModule
{
    public class Option
    {
        public Option()
        {
        }

        public string Path { get; set; }

        public int CommitThreshold { get; set; } = 1000;

        public bool DiscardLogs { get; set; } = false;
        public bool NoCommit { get; set; } = false;

        public bool MemWal { get; set; } = false;
    }
}