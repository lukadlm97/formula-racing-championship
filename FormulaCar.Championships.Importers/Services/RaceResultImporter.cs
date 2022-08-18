using FormulaCar.Championships.Importers.Fetchers;
using FormulaCar.Championships.Service.Abstraction;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using FormulaCar.Championships.Importers.Configurations;

namespace FormulaCar.Championships.Importers.Services
{
    public class RaceResultImporter : BackgroundService
    {
        private readonly IRaceFetcher _raceFetcher;
        private readonly ILogger<CountryImporter> _logger;
        private readonly IServiceManager _serviceManager;
        private readonly CircuitMapperSettings _circuiteMapperSettings;
        private readonly ImportSettings _importSettings;

        public RaceResultImporter(IServiceManager serviceManager, IRaceFetcher raceFetcher, ILogger<CountryImporter> logger,IOptions<CircuitMapperSettings> options, IOptions<ImportSettings> importSettingsOptions)
        {
            _serviceManager = serviceManager;
            _raceFetcher = raceFetcher;
            _logger = logger;
            _circuiteMapperSettings = options.Value;
            _importSettings = importSettingsOptions.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var season = Int32.Parse(_importSettings.Year);
            var grandPrix = "Bahrain International Circuit";
            var results = await _raceFetcher.GetRaceResults(grandPrix, season);



            _logger.LogInformation("Import of race result started for: " + grandPrix + " " + season);
            foreach (var item in results)
            {
                item.Season = season;
                item.Circuite = grandPrix;
                if (await _serviceManager.RaceClassificationService.Exist(item.Driver, item.Circuite,
                        item.Season.ToString(), item.Postion))
                {
                    _logger.LogWarning("Result exist: "+item.Driver+item.Postion+item.Circuite+item.Season);
                }
                else
                {

                    var createdItem = await _serviceManager.RaceClassificationService.Create(item);
                    if (createdItem == null)
                    {
                        _logger.LogInformation("Not created: "+item.Driver+" "+item.Postion);
                        continue;
                    }
                    var report = createdItem.Driver + " " + createdItem.Constructor + " " + createdItem.Position;
                    _logger.LogInformation("Result created: "+report);
                }
                
            }
            _logger.LogInformation("Completed import of race result for: "+grandPrix+" "+season);
        }
    }
}
