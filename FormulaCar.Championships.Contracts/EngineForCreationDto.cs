using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace FormulaCar.Championships.Contracts
{
    public class EngineForCreationDto
    {
        public string Manufacturer { get; set; }
        public bool IsActive { get; set; }
        public bool FirstRun { get; set; }
        public string Country { get; set; }
        
    }
}
