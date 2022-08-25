using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaCar.Championships.Domain.Entities
{
    public class Engine
    {
        public int Id { get; set; }
        public string Manufacturer { get; set; }
        public int CountryId { get; set; }
        public bool IsActive { get; set; } = false;
        public bool? FirstRun { get; set; }
    }
}
