using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaCar.Championships.Contracts
{
    public class RaceSpeedTrapForCreation
    {
        public string Driver { get; set; }
        public string Circuite { get; set; }
        public string Postion { get; set; }
        public double MaxSpeed { get; set; }
        public int Season { get; set; }
    }
}
