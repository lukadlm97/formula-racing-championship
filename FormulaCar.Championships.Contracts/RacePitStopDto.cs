using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaCar.Championships.Contracts
{
    public class RacePitStopDto
    {
        public int Count { get; set; }
        public TimeSpan TotalTime { get; set; }
        public string Position { get; set; }
        public string Driver { get; set; }
        public string Constructor { get; set; }
        public string Circuite { get; set; }
    }
}