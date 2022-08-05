using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaCar.Championships.Domain.Entities
{
    public class Raceweek
    {
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public bool IsContainsSprintQualification  { get; set; }
        public Circuite Circuite { get; set; }
        public int CircuiteId { get; set; }
        public Season Season { get; set; }
        public int SeasonId { get; set; }
    }
}
