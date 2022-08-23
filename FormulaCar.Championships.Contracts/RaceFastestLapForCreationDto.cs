using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaCar.Championships.Contracts
{
    public class RaceFastestLapForCreationDto
    {
        public TimeSpan LapTime { get; set; }
        public int Lap { get; set; }
        public TimeSpan Gap { get; set; }
        public double AvgSpeed { get; set; }
        public DateTime RegistrationTime { get; set; }
        public string Driver { get; set; }
        public string Circuite { get; set; }
        public string Postion { get; set; }

        public int Season { get; set; }
    }
}