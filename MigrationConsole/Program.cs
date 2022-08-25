using FormulaCar.Championships.Domain.Repositories;
using FormulaCar.Championships.Importers.Configurations;
using FormulaCar.Championships.Importers.Fetchers;
using FormulaCar.Championships.Importers.Loaders;
using FormulaCar.Championships.Importers.Services;
using FormulaCar.Championships.Persistence;
using FormulaCar.Championships.Persistence.Repositories;
using FormulaCar.Championships.Service;
using FormulaCar.Championships.Service.Abstraction;
using FormulaCar.Championships.Service.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

var hostBuilder = CreateHostBuilder(null);

await hostBuilder.Build().RunAsync();








static IHostBuilder CreateHostBuilder(string[] args)
    => Host.CreateDefaultBuilder(args)
        .ConfigureServices(services =>
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();

            services.AddDbContextPool<RepositoryDbContext>(builder =>
            {
                var connectionString = configuration.GetConnectionString("Database");

                builder.UseSqlServer(connectionString);
            });

            services.AddAutoMapper(typeof(PositionProfile));
            services.AddSingleton<IRepositoryManager, RepositoryManager>();
            services.AddSingleton<IServiceManager, ServiceManager>();
            services.AddSingleton<ICsvLoader, CsvLoader>();
            services.AddSingleton<IJsonLoader, JsonLoader>();
            services.AddSingleton<IConstructorFetcher, ConstructorFetcher>();
            services.AddSingleton<IBookingfetcher, BookingFetcher>();
            services.AddSingleton<IGrandPrixFetcher, GrandPrixFetcher>();
            services.AddSingleton<IFastestLapFetcher, FastestLapFetcher>();
            services.AddSingleton<ISpeedTrapFetcher, SpeedTrapFetcher>();
            services.AddSingleton<ISpeedTrapFetcher, SpeedTrapFetcher>();
            services.AddSingleton<IRaceMaximumSpeedFetcher, RaceMaximumSpeedFetcher>();
            services.AddSingleton<IRaceBestSectorTimesFetcher, RaceBestSectorTimesFetcher>();
            services.AddSingleton<IQualificationClassificationFetcher, QualificationClassificationFetcher>();
            services.AddSingleton<IQualificationSpeedTrapFetcher, QualificationSpeedTrapFetcher>();
            services.AddSingleton<IQualificationBestSectorTimeFetcher, QualificationBestSectorTimesFetcher>();
            services.AddSingleton<IQualificationMaximumSpeedFetcher, QualificationMaximumSpeedFetcher>();
            services.AddSingleton<IEngineFetcher, EngineFetcher>();
            services.AddSingleton<IRaceFetcher, RaceFetcher>();
            var importConfig = configuration.GetSection("ImporterConfiguration");
            var mapperConfig = configuration.GetSection("FiaTeamsMapping");
            var circuiteConfig = configuration.GetSection("FiaGrandPrixMapping");
            services.Configure<ImportSettings>(importConfig);
            services.Configure<TeamMapperSettings>(mapperConfig);
            services.Configure<CircuitMapperSettings>(circuiteConfig);
            services.AddHttpClient<ICircuitFetcher, CircuiteFetcher>(client =>
            {
                client.BaseAddress = new Uri(configuration["ImporterConfiguration:CircuiteSourceUrl"]);
            });
            services.AddHostedService<EngineImporter>();
        }).ConfigureLogging(loggingBuilder =>
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();
            loggingBuilder.ClearProviders();
            loggingBuilder.AddConfiguration(configuration.GetSection("Logging"));
            loggingBuilder.AddConsole();
            loggingBuilder.AddDebug();
        }).UseNLog();
