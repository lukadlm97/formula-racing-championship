using AutoMapper;
using FormulaCar.Championships.Contracts;
using FormulaCar.Championships.Domain.Entities;
using FormulaCar.Championships.Domain.Repositories;
using FormulaCar.Championships.Service.Abstraction;

namespace FormulaCar.Championships.Service;

public class RaceFastestLapService : IFastestLapService
{
    private readonly IMapper _mapper;
    private readonly IRepositoryManager _repositoryManager;

    public RaceFastestLapService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public Task<IEnumerable<RaceFastestLapDto>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Exist(string driver, string circuit, string season, string position)
    {
        var items =
            await GetDetails(driver, circuit, season, position);

        if (items.Item5 == null || items.Item3 == null || items.Item6 == null) return false;

        return (await _repositoryManager.RaceFastesLapRepository.FindByCondition(x
                =>
                x.BookingId == items.Item3.Id && x.RaceweekId == items.Item6.Id))
            .Any();
    }

    public async Task<RaceFastestLapDto> Create(RaceFastestLapForCreationDto raceFastestLapDto)
    {
        var items =
            await GetDetails(raceFastestLapDto.Driver, raceFastestLapDto.Circuite, raceFastestLapDto.Season.ToString(),
                raceFastestLapDto.Postion);

        if (items.Item5 == null || items.Item3 == null || items.Item6 == null) return null;

        var newRaceClassification = new RaceFastesLap
        {
            Lap = raceFastestLapDto.Lap,
            LapTime = raceFastestLapDto.LapTime,
            AvgSpeed = raceFastestLapDto.AvgSpeed,
            RegistrationTime = raceFastestLapDto.RegistrationTime,
            BookingId = items.Item3.Id,
            PositionId = items.Item5.Id,
            RaceweekId = items.Item6.Id
        };

        _repositoryManager.RaceFastesLapRepository.Insert(newRaceClassification);
        await _repositoryManager.UnitOfWork.SaveChangesAsync();

        return _mapper.Map<RaceFastestLapDto>(newRaceClassification);
    }

    private async Task<(Driver, Season, Booking, Circuite, Position, Raceweek)> GetDetails(string driver,
        string circuit, string season,
        string position)
    {
        var selectedPosition =
            (await _repositoryManager.PositionRepository.FindByCondition(x => x.Rank == position)).FirstOrDefault();
        var driverSplited = driver.Split(' ');
        var firstName = driverSplited[0];
        var lastName = driverSplited[1];
        var selectedDriver = (await _repositoryManager.DriverRepository.FindByCondition(x =>
                x.FirstName.ToLower() == firstName.ToLower() && x.LastName.ToLower() == lastName.ToLower()))
            .FirstOrDefault();
        if (selectedDriver == null)
            if (firstName.ToLower() == "Alexander".ToLower())
                firstName = "Alex";
        var selectedSeason =
            (await _repositoryManager.SeasonRepository.FindByCondition(x =>
                x.Year.ToString().ToLower() == season)).FirstOrDefault();
        selectedDriver = (await _repositoryManager.DriverRepository.FindByCondition(x =>
                x.FirstName.ToLower() == firstName.ToLower() && x.LastName.ToLower() == lastName.ToLower()))
            .FirstOrDefault();
        var selectedBooking = (await _repositoryManager.BookingRepository.FindByCondition(x =>
            x.DriverId == selectedDriver.Id && x.IsActive && x.SeasonId == selectedSeason.Id)).FirstOrDefault();
        var selectedCircuite = (
            await _repositoryManager.CircuiteRepository.FindByCondition(x =>
                x.Name.ToLower() == circuit)).FirstOrDefault();

        var selectedRaceweek = (await _repositoryManager.RaceweekRepository.FindByCondition(x =>
            x.CircuiteId == selectedCircuite.Id && x.SeasonId ==
            selectedSeason.Id)).OrderBy(x => x.OrderNumber).LastOrDefault();

        return (selectedDriver, selectedSeason, selectedBooking, selectedCircuite, selectedPosition, selectedRaceweek);
    }
}