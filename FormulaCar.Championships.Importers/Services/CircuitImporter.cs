using FormulaCar.Championships.Contracts;
using FormulaCar.Championships.Importers.Configurations;
using FormulaCar.Championships.Importers.Fetchers;
using FormulaCar.Championships.Service.Abstraction;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FormulaCar.Championships.Importers.Services;

public class CircuitImporter : BackgroundService
{
    private readonly ICircuitFetcher _circuitFetcher;
    private readonly ImportSettings _importSettings;
    private readonly ILogger<CountryImporter> _logger;
    private readonly IServiceManager _serviceManager;

    public CircuitImporter(IServiceManager serviceManager, ICircuitFetcher circuitFetcher,
        IOptions<ImportSettings> options, ILogger<CountryImporter> logger)
    {
        _serviceManager = serviceManager;
        _circuitFetcher = circuitFetcher;
        _logger = logger;
        _importSettings = options.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var exisitingCircuites = await _serviceManager.CircuiteService.GetAll();


        var circuites = await _circuitFetcher.GetCircuites();
        var uniqNamesOfCircuites = exisitingCircuites.Select(x => x.Name);
        foreach (var circuitDto in circuites)
        {
            if (uniqNamesOfCircuites.Contains(circuitDto.Name))

    {
                _logger.LogWarning(circuitDto.Name + " existing in DB");
                continue;
            }

            var countryId = await _serviceManager.CountryService.GetIdByName(circuitDto.CountryCode);
            if (countryId == -1)
            {
                _logger.LogWarning(circuitDto.Name + "[" + circuitDto.City + "]" +
                                   " not inserted. Could not be found country id for code:" + circuitDto.CountryCode);
                continue;
            }

            var newCircuit = new CircuitForCreationDto
            {
                City = circuitDto.City,
                CountryId = countryId,
                Name = circuitDto.Name
            };

            var createdCircuit = await _serviceManager.CircuiteService.Create(newCircuit);
            _logger.LogInformation(createdCircuit.Name + " created circuit with id:" + createdCircuit.CircuitId);
        }

        _logger.LogInformation(Enumerable.Repeat("=", 50).ToString());
        _logger.LogInformation("Import started!!!");

        /*foreach (var country in countries)
        {
            var countryForCreation = new CountryForCreationDto()
            {
                Code = country.Code,
                OriginalName = country.Name
            };
            var createdCountry = await _serviceManager.CountryService.Create(countryForCreation, stoppingToken);
            _logger.LogInformation("New country created :" + createdCountry.CountryId + "   [" + createdCountry.Name + "]");
        }*/


        _logger.LogInformation("Import ended!!!");
        _logger.LogInformation(Enumerable.Repeat("=", 50).ToString());
    }
}