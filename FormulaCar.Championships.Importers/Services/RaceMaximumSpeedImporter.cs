using FormulaCar.Championships.Importers.Configurations;
using FormulaCar.Championships.Importers.Fetchers;
using FormulaCar.Championships.Service.Abstraction;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaCar.Championships.Importers.Services
{
    public class RaceMaximumSpeedImporter : BackgroundService
    {
        private readonly IRaceMaximumSpeedFetcher _raceMaximumSpeedFetcher;
        private readonly ILogger<CountryImporter> _logger;
        private readonly IServiceManager _serviceManager;
        private readonly ImportSettings _importSettings;

        public RaceMaximumSpeedImporter(IServiceManager serviceManager, IRaceMaximumSpeedFetcher raceMaximumSpeedFetcher, ILogger<CountryImporter> logger, IOptions<ImportSettings> importSettingsOptions)
        {
            _serviceManager = serviceManager;
            _raceMaximumSpeedFetcher = raceMaximumSpeedFetcher;
            _logger = logger;
            _importSettings = importSettingsOptions.Value;
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

                var result = await _raceMaximumSpeedFetcher.GetRaceMaximumSpeeds(grandPrix, _importSettings.Year);

                Console.WriteLine(raceweek + "        " + _importSettings.Year);

                if (result == null)
                {
                    _logger.LogWarning("NOT AVAILABLE: " + raceweek + "        " + _importSettings.Year);
                    continue;
                }
                foreach (var raceMaximumSpeedForCreationDto in result)
                {
                    if (await _serviceManager.RaceMaximumSpeedService.Exist(raceMaximumSpeedForCreationDto.Driver, raceMaximumSpeedForCreationDto.Circuite,
                            raceMaximumSpeedForCreationDto.Season.ToString(), raceMaximumSpeedForCreationDto.Postion, raceMaximumSpeedForCreationDto.Sector))
                    {
                        _logger.LogWarning($"Exist max speed for:{raceMaximumSpeedForCreationDto.Postion}||| {raceMaximumSpeedForCreationDto.Driver}. {raceMaximumSpeedForCreationDto.Circuite}     {_importSettings.Year}");

                        continue;
                    }
                    var createdFastestLap = await _serviceManager.RaceMaximumSpeedService.Create(raceMaximumSpeedForCreationDto);
                    if (createdFastestLap == null)
                    {
                        _logger.LogWarning($"Exist  max speed  for:{raceMaximumSpeedForCreationDto.Postion}||| {raceMaximumSpeedForCreationDto.Driver}. {raceMaximumSpeedForCreationDto.Circuite}     {_importSettings.Year}");
                    }
                    else
                    {
                        _logger.LogInformation("Created new  max speed  time!!!");
                    }
                }
            }
            _logger.LogInformation("Import ended!!!");
        }
    }
}
