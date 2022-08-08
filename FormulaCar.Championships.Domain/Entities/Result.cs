using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace FormulaCar.Championships.Domain.Entities
{
    public abstract class Result
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public int RaceweekId { get; set; }
        public Position Position { get; set; }
    }
}
