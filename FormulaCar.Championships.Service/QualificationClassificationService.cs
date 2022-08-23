using AutoMapper;
using FormulaCar.Championships.Contracts;
using FormulaCar.Championships.Domain.Entities;
using FormulaCar.Championships.Domain.Exceptions;
using FormulaCar.Championships.Domain.Repositories;
using FormulaCar.Championships.Service.Abstraction;

namespace FormulaCar.Championships.Service;

public class QualificationClassificationService : IQualificationClassificationService
{
    private readonly IMapper _mapper;
    private readonly IRepositoryManager _repositoryManager;

    public QualificationClassificationService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public async Task<IEnumerable<QualificationClassificationDto>> GetAll()
    {
        var results = await _repositoryManager.QualificationClassificationRepository.FindAll();
        var bookings = await _repositoryManager.BookingRepository.FindAll();
        var raceweeks = await _repositoryManager.RaceweekRepository.FindAll();
        var constructors = await _repositoryManager.ConstructorRepository.FindAll();
        var drivers = await _repositoryManager.DriverRepository.FindByCondition(x => x.IsActive);
        var circuite = await _repositoryManager.CircuiteRepository.FindAll();
        var position = await _repositoryManager.PositionRepository.FindAll();

        var qualificationResultItemDtos = new List<QualificationClassificationDto>();

        foreach (var raceClassification in results)
        {
            var raceResult = _mapper.Map<QualificationClassificationDto>(raceClassification);
            var selectedBooking = bookings.FirstOrDefault(x => x.Id == raceClassification.BookingId);
            if (selectedBooking == null) throw new ItemNotFoundException(raceClassification.Id);

            var selectedDriver = drivers.FirstOrDefault(x => x.Id == selectedBooking.DriverId);
            var selectedConstructor = constructors.FirstOrDefault(x => x.Id == selectedBooking.ConstructorId);

            var selectedRaceweek = raceweeks.FirstOrDefault(x => x.Id == raceClassification.RaceweekId);
            if (selectedRaceweek == null) throw new ItemNotFoundException(raceClassification.Id);

            var selectedCircute = circuite.FirstOrDefault(x => x.Id == selectedRaceweek.CircuiteId);


            if (selectedCircute == null || selectedDriver == null || selectedConstructor == null)
                throw new ItemNotFoundException(raceClassification.Id);

            raceResult.Driver = selectedDriver.FirstName + " " + selectedDriver.LastName;
            raceResult.Constructor = selectedConstructor.Name;
            raceResult.Circuite = selectedCircute.Name;
            raceResult.Position = position.FirstOrDefault(x => x.Id == raceClassification.PositionId).Rank;
            qualificationResultItemDtos.Add(raceResult);
        }

        return qualificationResultItemDtos;
    }

    public async
        Task<QualificationClassificationDto> Create(
            QualificationClassificationForCreationDto raceResultItemForCreationDto)
    {
        var items =
            await GetDetails(raceResultItemForCreationDto.Driver, raceResultItemForCreationDto.Circuite,
                raceResultItemForCreationDto.Season,
                raceResultItemForCreationDto.Position, 0, raceResultItemForCreationDto.QualificationPeriod[1].ToString());

        if (items.Item5 == null || items.Item3 == null || items.Item6 == null || items.Item7 == null) return null;

        var newQualificationResult = new QualificationClassification
        {
            BookingId = items.Item3.Id,
            PositionId = items.Item5.Id,
            RaceweekId = items.Item6.Id,
            QualificationPeriodId = items.Item7.Id,
            Laps = raceResultItemForCreationDto.Laps,
            Time = raceResultItemForCreationDto.Time
        };

        _repositoryManager.QualificationClassificationRepository.Insert(newQualificationResult);
        await _repositoryManager.UnitOfWork.SaveChangesAsync();

        return _mapper.Map<QualificationClassificationDto>(newQualificationResult);
    }

    public async Task<bool> Exist(string driver, string circuit, string season, string position, int period)
    {
        var items =
            await GetDetails(driver, circuit, season, position, 0, period.ToString());

        if (items.Item5 == null || items.Item3 == null || items.Item6 == null || items.Item7 == null) return false;

        return (await _repositoryManager.QualificationClassificationRepository.FindByCondition(x =>
            x.BookingId == items.Item3.Id && x.RaceweekId == items.Item6.Id &&
            x.QualificationPeriodId == items.Item7.Id)).Any();
    }

    private async Task<(Driver, Season, Booking, Circuite, Position, Raceweek, QualificationPeriod, Sector)> GetDetails(
        string driver, string circuit, string season,
        string position, int sector, string period)
    {
        var selectedPosition =
            (await _repositoryManager.PositionRepository.FindByCondition(x => x.Rank == position)).FirstOrDefault();
        var driverSplited = driver.Split(' ');
        var firstName = driverSplited[0];
        var lastName = driverSplited[1];
        var selectedDriver = (await _repositoryManager.DriverRepository.FindByCondition(x =>
                x.FirstName.ToLower() == firstName.ToLower() && x.LastName.ToLower() == lastName.ToLower()))
            .FirstOrDefault();

        var selectedSeason =
            (await _repositoryManager.SeasonRepository.FindByCondition(x =>
                x.Year.ToString().ToLower() == season)).FirstOrDefault(); if (selectedDriver == null)
            if (firstName.ToLower() == "Alexander".ToLower())
                firstName = "Alex";
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
            (await _repositoryManager.SectorRepository.FindByCondition(x =>
                x.OrderNumber.ToString() == sector.ToString()))
            .FirstOrDefault();

        return (selectedDriver, selectedSeason, selectedBooking, selectedCircuite, selectedPosition, selectedRaceweek,
            selectedQualificationPeriod, selectedSector);
    }
}