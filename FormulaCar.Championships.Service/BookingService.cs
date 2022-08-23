using AutoMapper;
using FormulaCar.Championships.Contracts;
using FormulaCar.Championships.Domain.Entities;
using FormulaCar.Championships.Domain.Exceptions;
using FormulaCar.Championships.Domain.Repositories;
using FormulaCar.Championships.Service.Abstraction;

namespace FormulaCar.Championships.Service;

public class BookingService : IBookingService
{
    private readonly IMapper _mapper;
    private readonly IRepositoryManager _repositoryManager;

    public BookingService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BookingDto>> GetAll()
    {
        var bookings = await _repositoryManager.BookingRepository.FindAll();
        var seasons = await _repositoryManager.SeasonRepository.FindAll();
        var drivers = await _repositoryManager.DriverRepository.FindAll();
        var constructor = await _repositoryManager.ConstructorRepository.FindAll();
        var bookingDtos = new List<BookingDto>();
        foreach (var booking in bookings)
        {
            var selectedConstructor = constructor.FirstOrDefault(x => x.Id == booking.ConstructorId);
            var selectedDriver = drivers.FirstOrDefault(x => x.Id == booking.DriverId);
            var selectedSeason = seasons.FirstOrDefault(x => x.Id == booking.SeasonId);
            var newBooking = new BookingDto
            {
                Season = selectedSeason.Year.ToString(),
                ConstructorName = selectedConstructor.Name,
                DriverName = selectedDriver.FirstName + " " + selectedDriver.LastName
            };
            bookingDtos.Add(newBooking);
        }

        return bookingDtos;
    }

    public async Task<BookingDto> Create(BookingForCreationDto bookingForCreationDto)
    {
        var seasons =
            await _repositoryManager.SeasonRepository.FindByCondition(x => x.Year == bookingForCreationDto.Year);
        var drivers = await _repositoryManager.DriverRepository.FindByCondition(x =>
            (x.FirstName.ToLower() + " " + x.LastName.ToLower()).Contains(bookingForCreationDto.Driver.ToLower()));

        var selectedSeason = seasons.FirstOrDefault();
        var selectedDriver = drivers.FirstOrDefault();
        var selectedConstructor = await FindConstructor(bookingForCreationDto.Constructor);

        if (selectedConstructor == null || selectedDriver == null || selectedSeason == null) return null;

        var newBooking = new Booking
        {
            ConstructorId = selectedConstructor.Id,
            DriverId = selectedDriver.Id,
            SeasonId = selectedSeason.Id,
            Start = DateTime.Now,
            End = DateTime.Now.AddDays(150)
        };

        _repositoryManager.BookingRepository.Insert(newBooking);
        await _repositoryManager.UnitOfWork.SaveChangesAsync();

        var createdBooking = new BookingDto
        {
            Season = selectedSeason.Year.ToString(),
            ConstructorName = selectedConstructor.Name,
            DriverName = selectedDriver.FirstName + " " + selectedDriver.LastName
        };

        return createdBooking;
    }

    public async Task<bool> Exist(string driver, string constructor, string season)
    {
        var seasons = await _repositoryManager.SeasonRepository.FindByCondition(x => x.Year.ToString() == season);
        var drivers = await _repositoryManager.DriverRepository.FindByCondition(x =>
            x.FirstName.ToLower() + " " + x.LastName.ToLower() == driver.ToLower());

        var selectedSeason = seasons.FirstOrDefault();
        var selectedDriver = drivers.FirstOrDefault();
        var selectedConstructor = await FindConstructor(constructor);


        if (selectedConstructor == null || selectedDriver == null || selectedSeason == null) return false;

        var bookings =
            await _repositoryManager.BookingRepository.FindByCondition(x => x.DriverId == selectedDriver.Id);
        return bookings.Any(x => x.ConstructorId == selectedConstructor.Id && x.SeasonId == selectedSeason.Id);
    }

    public async Task<IEnumerable<BookingDto>> GetBookingsBySeason(string season)
    {
        var seasons =
            await _repositoryManager.SeasonRepository.FindByCondition(x => x.Year.ToString() == season);
        var targetSeason = seasons.FirstOrDefault();
        if (targetSeason == null) throw new ItemNotFoundException(-1);
        var drivers = await _repositoryManager.DriverRepository.FindAll();
        var constructor = await _repositoryManager.ConstructorRepository.FindAll();

        var bookings =
            await _repositoryManager.BookingRepository.FindByCondition(x => x.SeasonId == targetSeason.Id);

        var bookingDtos = new List<BookingDto>();
        foreach (var booking in bookings)
        {
            var selectedConstructor = constructor.FirstOrDefault(x => x.Id == booking.ConstructorId);
            var selectedDriver = drivers.FirstOrDefault(x => x.Id == booking.DriverId);
            var newBooking = new BookingDto
            {
                Season = targetSeason.Year.ToString(),
                ConstructorName = selectedConstructor.Name,
                DriverName = selectedDriver.FirstName + " " + selectedDriver.LastName
            };
            bookingDtos.Add(newBooking);
        }

        return bookingDtos;
    }


    private async Task<Constructor> FindConstructor(string constructorName)
    {
        var constructors = await _repositoryManager.ConstructorRepository.FindAll();

        foreach (var constructor in constructors)
        {
            var splitedConstructor = constructor.Name.Split('-').FirstOrDefault();

            if (splitedConstructor.ToLower().Contains(constructorName.ToLower())) return constructor;
        }

        return null;
    }
}