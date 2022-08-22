using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaCar.Championships.Domain.Entities
{
    public class QualificationMaximumSpeed : Result
    {
        public double MaxAvgSpeed { get; set; }
        public int SectorId { get; set; }
    }
}
