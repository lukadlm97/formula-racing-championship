using System.Threading.Channels;
using AutoMapper;
using FormulaCar.Championships.Contracts;
using FormulaCar.Championships.Domain.Entities;
using FormulaCar.Championships.Domain.Exceptions;
using FormulaCar.Championships.Domain.Repositories;
using FormulaCar.Championships.Service.Abstraction;

namespace FormulaCar.Championships.Service;

public class RaceBestSectorService : IRaceBestSectorService
{
    private readonly IMapper _mapper;
    private readonly IRepositoryManager _repositoryManager;

    public RaceBestSectorService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RaceBestSectorTimesDto>> GetAll()
    {
        var bestSectorClassification = await _repositoryManager.BestSectorsRepository.FindAll();
        var bookings = await _repositoryManager.BookingRepository.FindAll();
        var raceweeks = await _repositoryManager.RaceweekRepository.FindAll();
        var constructors = await _repositoryManager.ConstructorRepository.FindAll();
        var drivers = await _repositoryManager.DriverRepository.FindByCondition(x => x.IsActive);
        var circuite = await _repositoryManager.CircuiteRepository.FindAll();
        var position = await _repositoryManager.PositionRepository.FindAll();

        var raceResultItemDtos = new List<RaceBestSectorTimesDto>();

        foreach (var singleClassification in bestSectorClassification)
        {
            var sectorResult = _mapper.Map<RaceBestSectorTimesDto>(singleClassification);
            var selectedBooking = bookings.FirstOrDefault(x => x.Id == singleClassification.BookingId);
            if (selectedBooking == null) throw new ItemNotFoundException(singleClassification.Id);

            var selectedDriver = drivers.FirstOrDefault(x => x.Id == selectedBooking.DriverId);
            var selectedConstructor = constructors.FirstOrDefault(x => x.Id == selectedBooking.ConstructorId);

            var selectedRaceweek = raceweeks.FirstOrDefault(x => x.Id == singleClassification.RaceweekId);
            if (selectedRaceweek == null) throw new ItemNotFoundException(singleClassification.Id);

            var selectedCircute = circuite.FirstOrDefault(x => x.Id == selectedRaceweek.CircuiteId);

            var selectedPosition = position.FirstOrDefault(x => x.Id == singleClassification.PositionId);
            if (selectedCircute == null || selectedDriver == null || selectedConstructor == null ||
                selectedPosition == null) throw new ItemNotFoundException(singleClassification.Id);

            sectorResult.Driver = selectedDriver.FirstName + " " + selectedDriver.LastName;
            sectorResult.Constructor = selectedConstructor.Name;
            sectorResult.Circuite = selectedCircute.Name;
            sectorResult.Position = selectedPosition.Rank;
            raceResultItemDtos.Add(sectorResult);
        }

        return raceResultItemDtos;
    }

    public async Task<RaceBestSectorTimesDto> Create(RaceBestSectorTimesForCreationDto raceSectorsItem)
    {
        var items =
            await GetDetails(raceSectorsItem.Driver, raceSectorsItem.Circuite, raceSectorsItem.Season.ToString(),
                raceSectorsItem.Postion, raceSectorsItem.Sector);

        if (items.Item5 == null || items.Item3 == null || items.Item6 == null || items.Item7==null) return null;

        var newRaceSectorResult = new RaceSectorTime
        {
            BookingId = items.Item3.Id,
            PositionId = items.Item5.Id,
            RaceweekId = items.Item6.Id,
            SectorId = items.Item7.Id,
            Time = raceSectorsItem.Time
        };

        _repositoryManager.BestSectorsRepository.Insert(newRaceSectorResult);
        var changes= await _repositoryManager.UnitOfWork.SaveChangesAsync();
        Console.WriteLine(changes);
        return _mapper.Map<RaceBestSectorTimesDto>(newRaceSectorResult);
    }

    public async Task<bool> Exist(string driver, string circuit, string season, string position, int sector)
    {
        var items =
            await GetDetails(driver, circuit, season, position, sector);

        if (items.Item5 == null || items.Item3 == null || items.Item6 == null || items.Item7 == null) return false;

        return (await _repositoryManager.BestSectorsRepository.FindByCondition(x=>x.BookingId==items.Item3.Id&&x.RaceweekId==items.Item6.Id&&x.SectorId==items.Item7.Id)).Any();
    }


    private async Task<(Driver, Season, Booking, Circuite, Position, Raceweek, Sector)> GetDetails(string driver,
        string circuit, string season,
        string position, int sector)
    {
        var selectedPosition =
            (await _repositoryManager.PositionRepository.FindByCondition(x => x.Rank == position)).FirstOrDefault();
        var selectedDriver =
            (await _repositoryManager.DriverRepository.FindByCondition(x => x.LastName.ToLower() == driver.ToLower() && x.IsActive))
            .FirstOrDefault();

        var selectedSeason =
            (await _repositoryManager.SeasonRepository.FindByCondition(x =>
                x.Year.ToString().ToLower() == season)).FirstOrDefault();
        var selectedBooking = (await _repositoryManager.BookingRepository.FindByCondition(x =>
            x.DriverId == selectedDriver.Id && x.IsActive && x.SeasonId == selectedSeason.Id)).FirstOrDefault();
        var selectedCircuite = (
            await _repositoryManager.CircuiteRepository.FindByCondition(x =>
                x.Name.ToLower() == circuit)).FirstOrDefault();

        var selectedRaceweek = (await _repositoryManager.RaceweekRepository.FindByCondition(x =>
            x.CircuiteId == selectedCircuite.Id && x.SeasonId ==
            selectedSeason.Id)).OrderBy(x => x.OrderNumber).LastOrDefault();

        var selectedSector =
            (await _repositoryManager.SectorRepository.FindByCondition(x => x.OrderNumber == sector))
            .FirstOrDefault();

        return (selectedDriver, selectedSeason, selectedBooking, selectedCircuite, selectedPosition, selectedRaceweek,
            selectedSector);
    }
}