using FormulaCar.Championships.Contracts;
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
    public class QualificationClassificationImporter : BackgroundService
    {
        private readonly IQualificationClassificationFetcher _qualificationClassificationFetcher;
        private readonly ImportSettings _importSettings;
        private readonly ILogger<ConstructorImporter> _logger;
        private readonly IServiceManager _serviceManager;

        public QualificationClassificationImporter(IServiceManager serviceManager, IQualificationClassificationFetcher qualificationClassificationFetcher,
            IOptions<ImportSettings> options, ILogger<ConstructorImporter> logger)
        {
            _serviceManager = serviceManager;
            _qualificationClassificationFetcher = qualificationClassificationFetcher;
            _logger = logger;
            _importSettings = options.Value;
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

                var result = await _qualificationClassificationFetcher.Get(grandPrix, _importSettings.Year);

                Console.WriteLine(raceweek + "        " + _importSettings.Year);

                if (result == null)
                {
                    _logger.LogWarning("NOT AVAILABLE: " + raceweek + "        " + _importSettings.Year);
                    continue;
                }
                foreach (var sectorTime in result)
                {
                    /*
                    if (await _serviceManager.RaceBestSectorService.Exist(sectorTime.Driver, sectorTime.Circuite,
                            _importSettings.Year, sectorTime.Position, sector))
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
                    }*/
                }
            }
            _logger.LogInformation("Import ended!!!");
        }
    }
}
