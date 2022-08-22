using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaCar.Championships.Contracts
{
    public class RacePitStopForCreation
    {
        public int Count { get; set; }
        public TimeSpan TotalTime { get; set; }
        public string Driver { get; set; }
        public string Circuite { get; set; }
        public string Postion { get; set; }
        public int Season { get; set; }
    }
}
