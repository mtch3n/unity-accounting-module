using ConfigModule.Game;
using ConfigModule.Machine;

namespace ConfigModule
{
    public class Config
    {
        public Variable Variable { get; set; }
        public Remote Remote { get; set; }
        public Info Info { get; set; }
        public BillReader BillReader { get; set; }
    }
}