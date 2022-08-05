


using FormulaCar.Championships.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var hostBuilder = CreateHostBuilder(null);
await hostBuilder.Build().RunAsync();











 static IHostBuilder CreateHostBuilder(string[] args)
    => Host.CreateDefaultBuilder(args)
        .ConfigureServices(services =>
        {
            services.AddDbContext<RepositoryDbContext>(options=>options.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=formula-racing;Trusted_Connection=True;"));
        });
