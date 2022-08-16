using FormulaCar.Championships.Contracts;
using FormulaCar.Championships.Importers.Configurations;
using FormulaCar.Championships.Importers.Fetchers;
using FormulaCar.Championships.Service.Abstraction;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FormulaCar.Championships.Importers.Services;

public class BookingImporter : BackgroundService
{
    private readonly IBookingfetcher _bookingFetcher;
    private readonly IBookingService _bookingService;
    private readonly ImportSettings _importSettings;
    private readonly ILogger<CountryImporter> _logger;
    private readonly IServiceManager _serviceManager;

    public BookingImporter(IServiceManager serviceManager, IBookingfetcher bookingfetcher,
        IOptions<ImportSettings> options, ILogger<CountryImporter> logger)
    {
        _serviceManager = serviceManager;
        _bookingFetcher = bookingfetcher;
        _logger = logger;
        _importSettings = options.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("import Bookings started!!!");
        var existingBookings = await _serviceManager.BookingService.GetAll();


        var newBookings = await _bookingFetcher.GetBookings();

        _logger.LogInformation("loaded " + newBookings.Count() + " constructors");


        foreach (var booking in newBookings)
        {
            var splitedDriver = booking.DriverName.Split('.');
            booking.DriverName = splitedDriver.LastOrDefault();
            booking.DriverName = booking.DriverName.Trim(' ');
            var splitedConstructor = booking.ConstructorName.Split('-');
            booking.ConstructorName = splitedConstructor.FirstOrDefault();
            booking.ConstructorName = booking.ConstructorName.Trim('\0');
            booking.ConstructorName = booking.ConstructorName.Trim(' ');
            if (!await _serviceManager.BookingService.Exist(booking.DriverName, booking.ConstructorName,
                    booking.Season))
            {
                var insertBooking = new BookingForCreationDto
                {
                    Year = int.Parse(booking.Season),
                    Constructor = booking.ConstructorName,
                    Driver = booking.DriverName
                };
                var newBooking = await _serviceManager.BookingService.Create(insertBooking);
                if (newBooking != null)
                {
                    var report = newBooking.DriverName + ". " + newBooking.ConstructorName + " [" +
                                 newBooking.Season + "]";
                    Console.WriteLine(report);
                    _logger.LogInformation(report);
                }
                else
                {
                    _logger.LogInformation("Not imported: " + booking.DriverName + " " + booking.ConstructorName);
                }
            }
            else
            {
                _logger.LogInformation("Booking exist: " + booking.DriverName + " " + booking.ConstructorName + " " +
                                       booking.Season);
            }
        }

        _logger.LogInformation("import Bookings ended!!!");
    }
}