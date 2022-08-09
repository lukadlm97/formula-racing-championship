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
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<IJsonLoader, JsonLoader>();
            var importConfig = configuration.GetSection("ImporterConfiguration");
            services.Configure<ImportSettings>(importConfig);
            services.AddHttpClient<ICircuitFetcher, CircuiteFetcher>();
            services.AddHostedService<CircuitImporter>();
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
