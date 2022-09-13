namespace Report
{
    public class Option
    {
        public Option()
        {
            CommitThreshold = 100;
        }

        public string Path { get; set; }
        public int CommitThreshold { get; set; }
        public int DiscardLogs { get; set; }
    }
}