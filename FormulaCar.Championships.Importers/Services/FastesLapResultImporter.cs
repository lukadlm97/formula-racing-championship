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
using Microsoft.Office.Interop.Excel;
using FormulaCar.Championships.Contracts;

namespace FormulaCar.Championships.Importers.Services
{
    public class FastesLapResultImporter : BackgroundService
    {
        private readonly IFastestLapFetcher _fastestLapFetcher;
        private readonly ILogger<CountryImporter> _logger;
        private readonly IServiceManager _serviceManager;
        private readonly CircuitMapperSettings _circuiteMapperSettings;
        private readonly ImportSettings _importSettings;
        private readonly CircuitMapperSettings _circuteMapper;

        public FastesLapResultImporter(IServiceManager serviceManager, IFastestLapFetcher fastestLapFetcher, ILogger<CountryImporter> logger, IOptions<CircuitMapperSettings> options, IOptions<ImportSettings> importSettingsOptions,IOptions<CircuitMapperSettings> circuteMapperOptions)
        {
            _serviceManager = serviceManager;
            _fastestLapFetcher = fastestLapFetcher;
            _logger = logger;
            _circuiteMapperSettings = options.Value;
            _importSettings = importSettingsOptions.Value;
            _circuteMapper = circuteMapperOptions.Value;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var raceweeks =
                (await _serviceManager.RaceweekService.GetAll()).Where(x => x.Season == _importSettings.Year).OrderBy(x=>x.No).Select(x=>x.GrandPrixName);

            _logger.LogInformation("Import started!!!");
            foreach (var raceweek in raceweeks)
            {
                var result = await _fastestLapFetcher.GetFastestLaps(raceweek, _importSettings.Year);
                Console.WriteLine(raceweek+"        "+ _importSettings.Year);

                if (result == null)
                {
                    _logger.LogWarning("NOT AVAILABLE: "+raceweek + "        " + _importSettings.Year);
                    continue;
                }
                foreach (var raceFastestLapForCreationDto in result)
                {
                    if (await _serviceManager.FastestLapService.Exist(raceFastestLapForCreationDto.Driver,
                            raceFastestLapForCreationDto.Circuite, raceFastestLapForCreationDto.Season.ToString(),
                            raceFastestLapForCreationDto.Postion))
                    {
                        _logger.LogWarning($"Exist fastest lap for:{raceFastestLapForCreationDto.Postion}||| {raceFastestLapForCreationDto.Driver}. {raceFastestLapForCreationDto.Circuite}     {raceFastestLapForCreationDto.Season.ToString()}");
                        continue;
                    }

                    var createdFastestLap = await _serviceManager.FastestLapService.Create(raceFastestLapForCreationDto);
                    if (createdFastestLap == null)
                    {
                        _logger.LogWarning($"Exist fastest lap for:{raceFastestLapForCreationDto.Postion}||| {raceFastestLapForCreationDto.Driver}. {raceFastestLapForCreationDto.Circuite}     {raceFastestLapForCreationDto.Season.ToString()}");
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
