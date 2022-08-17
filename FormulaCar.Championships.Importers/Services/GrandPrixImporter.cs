using FormulaCar.Championships.Importers.Loaders;
using FormulaCar.Championships.Service.Abstraction;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormulaCar.Championships.Importers.Fetchers;
using FormulaCar.Championships.Contracts;
using FormulaCar.Championships.Importers.Configurations;
using Microsoft.Extensions.Options;

namespace FormulaCar.Championships.Importers.Services
{
    public class GrandPrixImporter : BackgroundService
    {
        private readonly IGrandPrixFetcher _grandPrixFetcher;
        private readonly ILogger<CountryImporter> _logger;
        private readonly IServiceManager _serviceManager;

        public GrandPrixImporter(IServiceManager serviceManager, IGrandPrixFetcher grandPrixFetcher, ILogger<CountryImporter> logger)
        {
            _serviceManager = serviceManager;
            _grandPrixFetcher = grandPrixFetcher;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("import raceweeks started!!!"); 
            //var existingBookings = await _serviceManager.RaceweekService.GetAll();


            var newRaceweeks = await _grandPrixFetcher.GetGrandPrix();

            _logger.LogInformation("loaded " + newRaceweeks.Count() + " raceweeks");


            foreach (var raceweek in newRaceweeks)
            {
                if (!await _serviceManager.RaceweekService.Exist(raceweek.GrandPrixName, raceweek.Season))
                {
                    var newRaceweek = await _serviceManager.RaceweekService.Create(raceweek);
                    if (newRaceweek != null)
                    {
                        var report = newRaceweek.No + ". " + newRaceweek.GrandPrixName + " [" +
                                     newRaceweek.Season + "]";
                        Console.WriteLine(report);
                        _logger.LogInformation(report);
                    }
                    else
                    {
                        _logger.LogInformation("Not imported: " + raceweek.Season + " " + raceweek.GrandPrixName);
                    }
                }
                else
                {
                    _logger.LogInformation("Raceweeks exist: " + raceweek.Season + " " + raceweek.GrandPrixName );
                }
            }

            _logger.LogInformation("import Raceweeks ended!!!");
        }
    }
}
