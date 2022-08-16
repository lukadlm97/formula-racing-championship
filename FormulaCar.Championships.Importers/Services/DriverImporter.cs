using FormulaCar.Championships.Contracts;
using FormulaCar.Championships.Importers.Loaders;
using FormulaCar.Championships.Service.Abstraction;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FormulaCar.Championships.Importers.Services;

public class DriverImporter : BackgroundService
{
    private readonly ICsvLoader _csvLoader;
    private readonly ILogger<CountryImporter> _logger;
    private readonly IServiceManager _serviceManager;

    public DriverImporter(IServiceManager serviceManager, ICsvLoader csvLoader, ILogger<CountryImporter> logger)
    {
        _serviceManager = serviceManager;
        _csvLoader = csvLoader;
        _logger = logger;
    }


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("import drivers started!!!");

        var newDrivers = _csvLoader.GetDrivers();

        _logger.LogInformation("loaded " + newDrivers.Count() + " drivers");

        foreach (var driverImportFormat in newDrivers)
            if (!await _serviceManager.DriverService.Exist(driverImportFormat.FirstName,
                    driverImportFormat.LastName))
            {
                var insertDriver = new DriverForCreationDto
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
                    _logger.LogInformation("Not imported: " + insertDriver.FirstName + " " + insertDriver.LastName +
                                           " [" + insertDriver.Nationality + "]");
                }
            }
            else
            {
                _logger.LogInformation("driver exist: " + driverImportFormat.FirstName + " " +
                                       driverImportFormat.LastName);
            }

        _logger.LogInformation("import drivers ended!!!");
    }
}