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

        public RaceResultImporter(IServiceManager serviceManager, IRaceFetcher raceFetcher, ILogger<CountryImporter> logger,IOptions<CircuitMapperSettings> options)
        {
            _serviceManager = serviceManager;
            _raceFetcher = raceFetcher;
            _logger = logger;
            _circuiteMapperSettings = options.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var season = 2022;
            var grandPrix ="Bahrain International Circuit";
            var results = await _raceFetcher.GetRaceResults(grandPrix, 2022);



            foreach (var item in results)
            {
                item.Season = season;
                item.Circuite = grandPrix;
                var createdItem = await _serviceManager.RaceClassificationService.Create(item);
                Console.WriteLine(createdItem.Driver+" "+ createdItem.Constructor+" "+ createdItem.Position);
            }
        }
    }
}
