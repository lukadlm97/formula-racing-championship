using FormulaCar.Championships.Importers.Configurations;
using FormulaCar.Championships.Importers.Fetchers;
using FormulaCar.Championships.Service.Abstraction;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaCar.Championships.Importers.Services
{
    public class RacePitStopImporter : BackgroundService
    {
        private readonly IPitStopFetcher _pitStopFetcher;
        private readonly ILogger<CountryImporter> _logger;
        private readonly IServiceManager _serviceManager;
        private readonly CircuitMapperSettings _circuiteMapperSettings;
        private readonly ImportSettings _importSettings;
        private readonly CircuitMapperSettings _circuteMapper;

        public RacePitStopImporter(IServiceManager serviceManager, IPitStopFetcher pitStopFetcher, ILogger<CountryImporter> logger, IOptions<CircuitMapperSettings> options, IOptions<ImportSettings> importSettingsOptions, IOptions<CircuitMapperSettings> circuteMapperOptions)
        {
            _serviceManager = serviceManager;
            _pitStopFetcher = pitStopFetcher;
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
                /* if (counter == 8)
                 {
                     continue;
                 }
                 if (_importSettings.Year == 2021.ToString()&&counter==9)
                 {
                     grandPrix = "Red Bull Ring 1";
                 }
                */

                var result = await _pitStopFetcher.Get(grandPrix, _importSettings.Year); 
              
                Console.WriteLine(raceweek + "        " + _importSettings.Year);

                if (result == null)
                {
                    _logger.LogWarning("NOT AVAILABLE: " + raceweek + "        " + _importSettings.Year);
                    continue;
                }
                foreach (var raceFastestLapForCreationDto in result)
                {

                    /*  if (_importSettings.Year == 2021.ToString() && counter == 9)
                    {
                        raceFastestLapForCreationDto.Circuite = "Red Bull Ring";
                    }
               */
                    if (await _serviceManager.RacePitStopService.Exist(raceFastestLapForCreationDto.Driver,
                            raceFastestLapForCreationDto.Circuite, _importSettings.Year,
                            raceFastestLapForCreationDto.Postion))
                    {
                        _logger.LogWarning($"Exist fastest lap for:{raceFastestLapForCreationDto.Postion}||| {raceFastestLapForCreationDto.Driver}. {raceFastestLapForCreationDto.Circuite}     {_importSettings.Year.ToString()}");
                        continue;
                    }

                    var createdFastestLap = await _serviceManager.RacePitStopService.Create(raceFastestLapForCreationDto);
                    if (createdFastestLap == null)
                    {
                        _logger.LogWarning($"Exist fastest lap for:{raceFastestLapForCreationDto.Postion}||| {raceFastestLapForCreationDto.Driver}. {raceFastestLapForCreationDto.Circuite}     {_importSettings.Year}");
                    }
                    else
                    {
                        _logger.LogInformation("Created new fastest lap!!!");
                    }
                }
            }
            _logger.LogInformation("Import ended!!!");


        }
    }
}
