using AutoMapper;
using FormulaCar.Championships.Contracts;
using FormulaCar.Championships.Domain.Entities;
using FormulaCar.Championships.Domain.Repositories;
using FormulaCar.Championships.Service.Abstraction;

namespace FormulaCar.Championships.Service;

public class QualificationBestSectorTimeService : IQualificationBestSectorService
{
    private readonly IMapper _mapper;
    private readonly IRepositoryManager _repositoryManager;

    public QualificationBestSectorTimeService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public Task<IEnumerable<QualificationBestSectorTimeDto>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<QualificationBestSectorTimeDto> Create(
        QualificationBestSectorTimeForCreationDto raceResultItemForCreationDto)
    {
        var items =
            await GetDetails(raceResultItemForCreationDto.Driver, raceResultItemForCreationDto.Circuite,
                raceResultItemForCreationDto.Season,
                raceResultItemForCreationDto.Position, raceResultItemForCreationDto.Sector, 0.ToString());

        if (items.Item5 == null || items.Item3 == null || items.Item6 == null || items.Item8 == null) return null;

        var newQualificationResult = new QualificationBestSectorTimes
        {
            BookingId = items.Item3.Id,
            PositionId = items.Item5.Id,
            RaceweekId = items.Item6.Id,
            SectorId = items.Item8.Id,
            Time = raceResultItemForCreationDto.Time
        };

        _repositoryManager.QualificationBestSectorTimesRepository.Insert(newQualificationResult);
        await _repositoryManager.UnitOfWork.SaveChangesAsync();

        return _mapper.Map<QualificationBestSectorTimeDto>(newQualificationResult);
    }

    public async Task<bool> Exist(string driver, string circuit, string season, string position, int sector)
    {
        var items =
            await GetDetails(driver, circuit, season, position, sector, 0.ToString());

        if (items.Item5 == null || items.Item3 == null || items.Item6 == null || items.Item8 == null) return false;

        return (await _repositoryManager.QualificationBestSectorTimesRepository.FindByCondition(x =>
            x.BookingId == items.Item3.Id && x.RaceweekId == items.Item6.Id && x.SectorId == items.Item8.Id)).Any();
    }

    private async Task<(Driver, Season, Booking, Circuite, Position, Raceweek, QualificationPeriod, Sector)> GetDetails(
        string driver, string circuit, string season,
        string position, int sector, string period)
    {
        var selectedPosition =
            (await _repositoryManager.PositionRepository.FindByCondition(x => x.Rank == position)).FirstOrDefault();
        var selectedDriver =
            (await _repositoryManager.DriverRepository.FindByCondition(x => x.LastName.ToLower() == driver.ToLower()))
            .FirstOrDefault();

        var selectedSeason =
            (await _repositoryManager.SeasonRepository.FindByCondition(x =>
                x.Year.ToString().ToLower() == season)).FirstOrDefault();
        selectedDriver = (await _repositoryManager.DriverRepository.FindByCondition(x =>
            x.LastName.ToLower() == driver.ToLower())).FirstOrDefault();
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
            (await _repositoryManager.SectorRepository.FindByCondition(x =>
                x.OrderNumber.ToString() == sector.ToString()))
            .FirstOrDefault();

        return (selectedDriver, selectedSeason, selectedBooking, selectedCircuite, selectedPosition, selectedRaceweek,
            selectedQualificationPeriod, selectedSector);
    }
}