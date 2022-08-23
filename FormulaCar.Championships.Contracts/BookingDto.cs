using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace FormulaCar.Championships.Contracts
{
    public class BookingDto
    {
        public string DriverName { get; set; }
        public string ConstructorName { get; set; }
        public string Season { get; set; }
    }
}