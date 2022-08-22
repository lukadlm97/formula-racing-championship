using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaCar.Championships.Contracts
{
    public class RaceFastestLapDto
    {
        public string Position { get; set; }
        public string Driver { get; set; }
        public string Constructor { get; set; }
        public string Circuite { get; set; }
        public TimeSpan LapTime { get; set; }
        public int Lap { get; set; }
        public TimeSpan Gap { get; set; }
        public double AvgSpeed { get; set; }
        public DateTime RegistrationTime { get; set; }
    }
}
