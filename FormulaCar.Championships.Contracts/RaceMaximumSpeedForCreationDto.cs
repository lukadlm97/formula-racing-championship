using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaCar.Championships.Contracts
{
    public class RaceMaximumSpeedForCreationDto
    {
        public string Driver { get; set; }
        public string Circuite { get; set; }
        public string Postion { get; set; }
        public int Season { get; set; }
        public double MaxAvgSpeed { get; set; }
        public int Sector { get; set; }
    }
}
