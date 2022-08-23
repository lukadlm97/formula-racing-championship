using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaCar.Championships.Contracts
{
    public class QualificationClassificationForCreationDto
    {
        public string Driver { get; set; }
        public string Constructor { get; set; }
        public string Circuite { get; set; }
        public string Position { get; set; }
        public string Season { get; set; }
        public TimeSpan Time { get; set; }
        public int Laps { get; set; }
        public string QualificationPeriod { get; set; }
    }
}