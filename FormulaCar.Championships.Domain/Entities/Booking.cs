using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaCar.Championships.Domain.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public int ContactLenght { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool IsActive { get; set; }
        public int SeasonId { get; set; }
        public Season Season { get; set; }
        public int DriverId { get; set; }
        public Driver Driver { get; set; }
        public int ContructorId { get; set; }
        public Constructor Constructor { get; set; }
    }
}
