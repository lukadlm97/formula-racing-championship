using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaCar.Championships.Contracts
{
    public class QualificationPeriodsForCreationDto
    {
        public int OrderNumber { get; set; }
        public string PeriodName { get; set; }
        public string ShortPeriodName { get; set; }
    }
}
