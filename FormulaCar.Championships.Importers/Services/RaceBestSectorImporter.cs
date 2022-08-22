using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormulaCar.Championships.Importers.Configurations;
using FormulaCar.Championships.Importers.Fetchers;
using FormulaCar.Championships.Service.Abstraction;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FormulaCar.Championships.Importers.Services
{
    public class RaceBestSectorImporter:BackgroundService
    {
        private readonly IRaceBestSectorTimesFetcher _raceBestSectorTimesFetcher;
        private readonly ILogger<CountryImporter> _logger;
        private readonly IServiceManager _serviceManager;
        private readonly CircuitMapperSettings _circuiteMapperSettings;
        private readonly ImportSettings _importSettings;
        private readonly CircuitMapperSettings _circuteMapper;

        public RaceBestSectorImporter(IServiceManager serviceManager, IRaceBestSectorTimesFetcher raceBestSectorTimesFetcher, ILogger<CountryImporter> logger, IOptions<CircuitMapperSettings> options, IOptions<ImportSettings> importSettingsOptions, IOptions<CircuitMapperSettings> circuteMapperOptions)
        {
            _serviceManager = serviceManager;
            _raceBestSectorTimesFetcher = raceBestSectorTimesFetcher;
            _logger = logger;
            _circuiteMapperSettings = options.Value;
            _importSettings = importSettingsOptions.Value;
            _circuteMapper = circuteMapperOptions.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var raceweeks =
                (await _serviceManager.RaceweekService.GetAll()).Where(x => x.Season == _importSettings.Year).OrderBy(x => x.No).Select(x => x.GrandPrixName);

            _logger.LogInformation("Import started!!!");
            int counter = 0;
            foreach (var raceweek in raceweeks)
            {
                counter++;
                var grandPrix = raceweek;

                var result = await _raceBestSectorTimesFetcher.GetBestSectorTimes(grandPrix, _importSettings.Year);

                Console.WriteLine(raceweek + "        " + _importSettings.Year);

                if (result == null)
                {
                    _logger.LogWarning("NOT AVAILABLE: " + raceweek + "        " + _importSettings.Year);
                    continue;
                }
                foreach (var sectorTime in result)
                {
                    if (await _serviceManager.RaceBestSectorService.Exist(sectorTime.Driver, sectorTime.Circuite,
                            sectorTime.Season.ToString(), sectorTime.Postion, sectorTime.Sector))
                    {
                        _logger.LogWarning($"Exist sector time for:{sectorTime.Postion}||| {sectorTime.Driver}. {sectorTime.Circuite}     {_importSettings.Year}");

                        continue;
                    }
                    var createdFastestLap = await _serviceManager.RaceBestSectorService.Create(sectorTime);
                    if (createdFastestLap == null)
                    {
                        _logger.LogWarning($"Exist sector time for:{sectorTime.Postion}||| {sectorTime.Driver}. {sectorTime.Circuite}     {_importSettings.Year}");
                    }
                    else
                    {
                        _logger.LogInformation("Created new sector time!!!");
                    }
                }
            }
            _logger.LogInformation("Import ended!!!");
        }
    }
}
