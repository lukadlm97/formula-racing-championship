using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace FormulaCar.Championships.Domain.Entities
{
    public class RaceClassification:Result
    {
        public int Laps { get; set; }
        public TimeSpan Time { get; set; }
    }
}
