using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaCar.Championships.Domain.Entities
{
    public class QualificationClassification:Result
    {
        public TimeSpan Time { get; set; }
        public int Laps { get; set; }
        public int QualificationPeriodId { get; set; }

    }
}
