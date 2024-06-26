﻿namespace ConfigModule.Machine
{
    public class Info
    {
        public string TypeNo { get; set; }
        public string MachineNo { get; set; }
        public string ConfirmCode { get; set; }
        public long AccountingCount { get; set; }
        
        public long ResetCount { get; set; }

        public long AccountingTimestamp { get; set; }
        public string Password { get; set; }
    }
}