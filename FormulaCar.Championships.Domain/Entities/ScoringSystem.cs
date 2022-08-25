using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaCar.Championships.Domain.Entities
{
    public class ScoringSystem
    {
        public int Id { get; set; }
        public Raceweek Raceweek { get; set; }
        public RegulationRule RegulationRule { get; set; }
    }
}
