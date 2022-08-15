using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace FormulaCar.Championships.Importers.Configurations
{
    public class ImportSettings
    {
        public string CountrySourceFilePath { get; set; }
        public string CircuiteSourceUrl { get; set; }
        public string DriversCsv { get; set; }
        
    }
}
