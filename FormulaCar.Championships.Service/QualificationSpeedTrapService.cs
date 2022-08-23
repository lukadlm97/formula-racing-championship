using AutoMapper;
using FormulaCar.Championships.Contracts;
using FormulaCar.Championships.Domain.Entities;
using FormulaCar.Championships.Domain.Repositories;
using FormulaCar.Championships.Service.Abstraction;

namespace FormulaCar.Championships.Service;

public class QualificationSpeedTrapService : IQualificationSpeedTrapService
{
    private readonly IMapper _mapper;
    private readonly IRepositoryManager _repositoryManager;

    public QualificationSpeedTrapService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public async Task<IEnumerable<QualificationSpeedTrapDto>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<QualificationSpeedTrapDto> Create(
        QualificationSpeedTrapForCreationDto raceResultItemForCreationDto)
    {
        var items =
            await GetDetails(raceResultItemForCreationDto.Driver, raceResultItemForCreationDto.Circuite,
                raceResultItemForCreationDto.Season,
                raceResultItemForCreationDto.Position, 0.ToString(), 0.ToString());

        if (items.Item5 == null || items.Item3 == null || items.Item6 == null) return null;

        var newQualificationResult = new QualificationSpeedTrap
        {
            BookingId = items.Item3.Id,
            PositionId = items.Item5.Id,
            RaceweekId = items.Item6.Id,
            MaxSpeed = raceResultItemForCreationDto.MaxSpeed
        };

        _repositoryManager.QualificationSpeedTrapRepository.Insert(newQualificationResult);
        await _repositoryManager.UnitOfWork.SaveChangesAsync();

        return _mapper.Map<QualificationSpeedTrapDto>(newQualificationResult);
    }

    public async Task<bool> Exist(string driver, string circuit, string season, string position, int sector)
    {
        var items =
            await GetDetails(driver, circuit, season, position, sector.ToString(), 0.ToString());

        if (items.Item5 == null || items.Item3 == null || items.Item6 == null || items.Item8 == null) return false;

        return (await _repositoryManager.QualificationMaximumSpeedRepository.FindByCondition(x =>
            x.BookingId == items.Item3.Id && x.RaceweekId == items.Item6.Id && x.SectorId == items.Item8.Id)).Any();
    }

    private async Task<(Driver, Season, Booking, Circuite, Position, Raceweek, QualificationPeriod, Sector)> GetDetails(
        string driver, string circuit, string season,
        string position, string sector, string period)
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

        var selectedQualificationPeriod =
            (await _repositoryManager.QualificationPeriodsRepository.FindByCondition(x =>
                x.OrderNumber.ToString() == period))
            .FirstOrDefault();

        var selectedSector =
            (await _repositoryManager.SectorRepository.FindByCondition(x => x.OrderNumber.ToString() == sector))
            .FirstOrDefault();

        return (selectedDriver, selectedSeason, selectedBooking, selectedCircuite, selectedPosition, selectedRaceweek,
            selectedQualificationPeriod, selectedSector);
    }
}