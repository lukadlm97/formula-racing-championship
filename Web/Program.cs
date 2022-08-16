using FormulaCar.Championships.Domain.Repositories;
using FormulaCar.Championships.Persistence;
using FormulaCar.Championships.Persistence.Repositories;
using FormulaCar.Championships.Presentation;
using FormulaCar.Championships.Service;
using FormulaCar.Championships.Service.Abstraction;
using FormulaCar.Championships.Service.Mappers;
using Microsoft.EntityFrameworkCore;
using NLog.Web;
using Web.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
ConfigureServices(builder.Services, builder.Configuration);
ConfigureLogging(builder.Logging, builder.Configuration);
builder.Host.UseNLog();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapControllers();

app.Run();


void ConfigureServices(IServiceCollection serviceCollection, IConfiguration configuration)
{
    serviceCollection.AddControllers().AddApplicationPart(typeof(AssemblyReference).Assembly);
    serviceCollection.AddScoped<IServiceManager, ServiceManager>();

    serviceCollection.AddScoped<IRepositoryManager, RepositoryManager>();

    serviceCollection.AddDbContextPool<RepositoryDbContext>(builder =>
    {
        var connectionString = configuration.GetConnectionString("Database");

        builder.UseSqlServer(connectionString);
    });

    serviceCollection.AddAutoMapper(typeof(PositionProfile));
    serviceCollection.AddScoped<ExceptionHandlingMiddleware>();
}

void ConfigureLogging(ILoggingBuilder loggingBuilder, IConfiguration configuration)
{
    loggingBuilder.ClearProviders();
    loggingBuilder.AddConfiguration(configuration.GetSection("Logging"));
    loggingBuilder.AddConsole();
    loggingBuilder.AddDebug();
}