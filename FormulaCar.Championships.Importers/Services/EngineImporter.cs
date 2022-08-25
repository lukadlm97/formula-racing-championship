using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormulaCar.Championships.Importers.Configurations;
using FormulaCar.Championships.Importers.Fetchers;
using FormulaCar.Championships.Service.Abstraction;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace FormulaCar.Championships.Importers.Services
{
    public class EngineImporter:BackgroundService
    {
        private readonly IServiceManager _serviceManager;
        private readonly IEngineFetcher _engineFetcher;

        public EngineImporter(IEngineFetcher engineFetcher,IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
            _engineFetcher = engineFetcher;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var engines = await _engineFetcher.GetEngines();


            foreach (var engineForCreationDto in engines)
            {
                if (await _serviceManager.EngineService.Exist(engineForCreationDto.Manufacturer))
                {
                    Console.WriteLine("ENGINE EXIST!!!");
                    continue;
                }

                var newEngine = await _serviceManager.EngineService.Create(engineForCreationDto);
                if (newEngine == null)
                {
                    Console.WriteLine("NOT CREATED!!!");
                }
            }


        }
    }
}
