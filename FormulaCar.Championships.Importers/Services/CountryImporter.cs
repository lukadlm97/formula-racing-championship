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
    public class CountryImporter : BackgroundService
    {
        private readonly IServiceManager _serviceManager;
        private readonly IJsonLoader _jsonLoader;
        private readonly ImportSettings _importSettings;
        private readonly ILogger<CountryImporter> _logger;

        public CountryImporter(IServiceManager serviceManager,IJsonLoader jsonLoader,IOptions<ImportSettings> options,ILogger<CountryImporter> logger)
        {
            _serviceManager = serviceManager;
            _jsonLoader = jsonLoader;
            _logger = logger;
            _importSettings = options.Value;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var countries = await _serviceManager.CountryService.GetCountries(stoppingToken);
            if (countries != null && countries.Any())
            {
                _logger.LogInformation("Countries exist!!!");
                return;
            }

            countries = await _jsonLoader.GetCountries(_importSettings.CountrySourceFilePath);


            _logger.LogInformation(Enumerable.Repeat("=",50).ToString());
            _logger.LogInformation("Import started!!!");
            foreach (var country in countries)
            {
                var countryForCreation = new CountryForCreationDto()
                {
                    Code = country.Code,
                    OriginalName = country.Name
                };
                var createdCountry = await _serviceManager.CountryService.Create(countryForCreation, stoppingToken);
                _logger.LogInformation("New country created :"+createdCountry.CountryId+"   ["+createdCountry.Name+"]");
            }


            _logger.LogInformation("Import ended!!!");
            _logger.LogInformation(Enumerable.Repeat("=", 50).ToString());

        }
    }
}
