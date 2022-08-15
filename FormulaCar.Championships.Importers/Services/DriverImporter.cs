using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormulaCar.Championships.Contracts;
using FormulaCar.Championships.Importers.Configurations;
using FormulaCar.Championships.Importers.Loaders;
using FormulaCar.Championships.Service.Abstraction;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FormulaCar.Championships.Importers.Services
{
    public class DriverImporter:BackgroundService
    {
        private readonly IServiceManager _serviceManager;
        private readonly ICsvLoader _csvLoader;
        private readonly ILogger<CountryImporter> _logger;

        public DriverImporter(IServiceManager serviceManager, ICsvLoader csvLoader,ILogger<CountryImporter> logger)
        {
            _serviceManager = serviceManager;
            _csvLoader = csvLoader;
            _logger = logger;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            _logger.LogInformation("import drivers started!!!");
            var drivers = await _serviceManager.DriverService.GetDrivers();
            if (drivers != null && drivers.Any())
            {
                _logger.LogInformation("Countries exist!!!");
                return;
            }

            var newDrivers =  _csvLoader.GetDrivers();

            _logger.LogInformation("loaded "+newDrivers.Count()+" drivers");

            foreach (var driverImportFormat in newDrivers)
            {
                var insertDriver = new DriverForCreationDto()
                {
                    DateOfBirth = driverImportFormat.DateOfBirth,
                    FirstName = driverImportFormat.FirstName,
                    LastName = driverImportFormat.LastName,
                    Nationality = driverImportFormat.Country.Name,
                    IsActive = driverImportFormat.IsActive
                };
               var newDriver = await _serviceManager.DriverService.Create(insertDriver);
               if (newDriver != null)
               {
                   var report = newDriver.DriverId + ". " + newDriver.FirstName + " " + newDriver.LastName + " [" +
                                newDriver.Country + "]";
                   Console.WriteLine(report);
                   _logger.LogInformation(report);
                }
               else
               {

                   _logger.LogInformation("Not imported: "+insertDriver.FirstName+" "+insertDriver.LastName+" ["+insertDriver.Nationality+"]");
                }
             
            }

            _logger.LogInformation("import drivers ended!!!");

        }
    }
}
