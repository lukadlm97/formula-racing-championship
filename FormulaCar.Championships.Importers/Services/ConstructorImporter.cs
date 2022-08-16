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
    public class ConstructorImporter : BackgroundService
    {
        private readonly IConstructorFetcher _constructorFetcher;
        private readonly ImportSettings _importSettings;
        private readonly ILogger<ConstructorImporter> _logger;
        private readonly IServiceManager _serviceManager;

        public ConstructorImporter(IServiceManager serviceManager, IConstructorFetcher constructorFetcher,
            IOptions<ImportSettings> options, ILogger<ConstructorImporter> logger)
        {
            _serviceManager = serviceManager;
            _constructorFetcher = constructorFetcher;
            _logger = logger;
            _importSettings = options.Value;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("import constructors started!!!");

            var newConstructors = await _constructorFetcher.GetConstructors();

            _logger.LogInformation("loaded " + newConstructors.Count() + " constructors");

            foreach (var constructor in newConstructors)
                if (!await _serviceManager.ConstructorService.Exist(constructor.Name))
                {
                    var insertConstructor = new ConstructorForCreationDto()
                    {
                       CountryCode = constructor.Country,
                       FirstApperance = constructor.FirstApperance,
                       Name = constructor.Name
                    };
                    var newConstructor = await _serviceManager.ConstructorService.CreateConstructor(insertConstructor);
                    if (newConstructor != null)
                    {
                        var report = newConstructor.Name + ". " + newConstructor.Country +  " [" +
                                     newConstructor.FirstApperance + "]";
                        Console.WriteLine(report);
                        _logger.LogInformation(report);
                    }
                    else
                    {
                        _logger.LogInformation("Not imported: " + insertConstructor.Name + " " + insertConstructor.CountryCode);
                    }
                }
                else
                {
                    _logger.LogInformation("constructor exist: " + constructor.Name);
                }

            _logger.LogInformation("import constructors ended!!!");
        }
    }
}
