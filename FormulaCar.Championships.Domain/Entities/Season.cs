using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaCar.Championships.Domain.Entities
{
    public class Season
    {
        public int Id { get; set; } 
        public int Year { get; set; } 
        public int RaceNumber { get; set; }
        public IEnumerable<Booking> Bookings { get; set; }
        public IEnumerable<Raceweek> Raceweeks { get; set; }
    }
}
