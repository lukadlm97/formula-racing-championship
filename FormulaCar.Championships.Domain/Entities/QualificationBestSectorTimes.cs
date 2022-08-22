using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaCar.Championships.Domain.Entities
{
    public class QualificationBestSectorTimes:Result
    {
        public TimeSpan Time { get; set; }
        public int SectorId { get; set; }
    }
}
