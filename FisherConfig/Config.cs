using FisherConfig.Game;
using FisherConfig.Machine;
using FisherConfig.Report;

namespace FisherConfig
{
    public class Config
    {
        public Variable Variable { get; set; }
        public Remote Remote { get; set; }
        public Info Info { get; set; }
        public CurrentInfo CurrentInfo { get; set; }
    }
}