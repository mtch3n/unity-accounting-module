namespace Report
{
    public class Option
    {
        public string Path { get; set; }
        public int CommitThreshold { get; set; }
        public int DiscardLogs { get; set; }

        public Option()
        {
            CommitThreshold = 100;
        }
    }
}