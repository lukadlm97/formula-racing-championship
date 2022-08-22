namespace FormulaCar.Championships.Domain.Repositories;

public interface IRepositoryManager
{
    IBookingRepository BookingRepository { get; }
    ICircuiteRepository CircuiteRepository { get; }
    IConstructorRepository ConstructorRepository { get; }
    ICountryRepository CountryRepository { get; }
    IDriverRepository DriverRepository { get; }
    IMediaTagRepository MediaTagRepository { get; }
    IPositionRepository PositionRepository { get; }
    IQualificationPeriodsRepository QualificationPeriodsRepository { get; }
    IRaceClassificationRepository RaceClassificationRepository { get; }
    IRaceFastesLapRepository RaceFastesLapRepository { get; }
    IRaceSpeedTrapRepository RaceSpeedTrapRepository { get; }
    IRacePitStop RacePitStop { get; }
    IRaceweekRepository RaceweekRepository { get; }
    ISeasonRepository SeasonRepository { get; }
    ISectorRepository SectorRepository { get; }
    IUnitOfWork UnitOfWork { get; }
}