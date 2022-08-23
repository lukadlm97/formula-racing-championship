using FormulaCar.Championships.Domain.Entities;
using FormulaCar.Championships.Domain.Repositories;

namespace FormulaCar.Championships.Persistence.Repositories;

public class RepositoryManager : IRepositoryManager
{
    private readonly Lazy<IBookingRepository> _lazyBookingRepository;
    private readonly Lazy<ICircuiteRepository> _lazyCircuitRepository;
    private readonly Lazy<IConstructorRepository> _lazyConstructorRepository;
    private readonly Lazy<ICountryRepository> _lazyCountryRepository;
    private readonly Lazy<IDriverRepository> _lazyDriverRepository;
    private readonly Lazy<IMediaTagRepository> _lazyMediaTagRepository;
    private readonly Lazy<IPositionRepository> _lazyPositionRepository;
    private readonly Lazy<IQualificationPeriodsRepository> _lazyQualificationPeriodsRepository;
    private readonly Lazy<IRaceClassificationRepository> _lazyRaceClassificationRepository;
    private readonly Lazy<IRaceweekRepository> _lazyRaceweekRepository;
    private readonly Lazy<ISeasonRepository> _lazySeasonRepository;
    private readonly Lazy<ISectorRepository> _lazySectorRepository;
    private readonly Lazy<IRaceFastesLapRepository> _lazyFastestLapRepository;
    private readonly Lazy<IRacePitStop> _lazyPitStopRepository;
    private readonly Lazy<IRaceSpeedTrapRepository> _lazyRaceSpeedTrapRepository;
    private readonly Lazy<IRaceBestSectorsRepository> _lazyRaceBestSectorsRepository;
    private readonly Lazy<IRaceMaximumSpeedsRepository> _lazyRaceMaximumSpeedsRepository;
    private readonly Lazy<IQualificationSpeedTrapRepository> _lazyQualificationSpeedTrapRepository;
    private readonly Lazy<IQualificationMaximumSpeedRepository> _lazyQualificationMaximumSpeedRepository;
    private readonly Lazy<IQualificationClassificationRepository> _lazyQualificationClassificationRepository;
    private readonly Lazy<IQualificationBestSectorTimesRepository> _lazyQualificationBestSectorTimesRepository;
    private readonly Lazy<IUnitOfWork> _lazyUnitOfWork;

    public RepositoryManager(RepositoryDbContext repositoryDbContext)
    {
        _lazyBookingRepository = new Lazy<IBookingRepository>(() => new BookingRepository(repositoryDbContext));
        _lazyCircuitRepository = new Lazy<ICircuiteRepository>(() => new CircuiteRepository(repositoryDbContext));
        _lazyConstructorRepository =
            new Lazy<IConstructorRepository>(() => new ConstructorRepository(repositoryDbContext));
        _lazyCountryRepository = new Lazy<ICountryRepository>(() => new CountryRepository(repositoryDbContext));
        _lazyDriverRepository = new Lazy<IDriverRepository>(() => new DriversRepository(repositoryDbContext));
        _lazyMediaTagRepository = new Lazy<IMediaTagRepository>(() => new MediaTagRepository(repositoryDbContext));
        _lazyPositionRepository = new Lazy<IPositionRepository>(() => new PositionRepository(repositoryDbContext));
        //_lazyQualificationPeriodsRepository = new Lazy<IQualificationPeriodsRepository>(() => new qperiod(repositoryDbContext));
        _lazyRaceClassificationRepository =
            new Lazy<IRaceClassificationRepository>(() => new RaceClassificationRepository(repositoryDbContext));
        _lazyRaceweekRepository = new Lazy<IRaceweekRepository>(() => new RaceweekRepository(repositoryDbContext));
        _lazySeasonRepository = new Lazy<ISeasonRepository>(() => new SeasonRepository(repositoryDbContext));
        _lazySectorRepository = new Lazy<ISectorRepository>(() => new SectorsRepository(repositoryDbContext));
        _lazyFastestLapRepository = new Lazy<IRaceFastesLapRepository>(() => new RaceFastestLapRepository(repositoryDbContext));
        _lazyPitStopRepository = new Lazy<IRacePitStop>(() => new RacePitStopRepository(repositoryDbContext));
        _lazyRaceSpeedTrapRepository = new Lazy<IRaceSpeedTrapRepository>(() => new RaceSpeedTrapRepository(repositoryDbContext));
        _lazySectorRepository = new Lazy<ISectorRepository>(() => new SectorsRepository(repositoryDbContext));
        _lazySectorRepository = new Lazy<ISectorRepository>(() => new SectorsRepository(repositoryDbContext));
        _lazyRaceBestSectorsRepository = new Lazy<IRaceBestSectorsRepository>(() => new RaceBestSectorRepository(repositoryDbContext));
        _lazyRaceMaximumSpeedsRepository = new Lazy<IRaceMaximumSpeedsRepository>(() => new RaceMaximumSpeedsRepository(repositoryDbContext));
        _lazyQualificationPeriodsRepository =
            new Lazy<IQualificationPeriodsRepository>(() => new QualificationPeriodRepository(repositoryDbContext));
        _lazyQualificationBestSectorTimesRepository = new Lazy<IQualificationBestSectorTimesRepository>(() => new QualificationBestSectorTimesRepository(repositoryDbContext));
        _lazyQualificationClassificationRepository = new Lazy<IQualificationClassificationRepository>(() => new QualificationClassificationRepository(repositoryDbContext));
        _lazyQualificationMaximumSpeedRepository = new Lazy<IQualificationMaximumSpeedRepository>(() => new QualificationMaximumSpeedRepository(repositoryDbContext));
        _lazyQualificationSpeedTrapRepository = new Lazy<IQualificationSpeedTrapRepository>(() => new QualificationSpeedTrapRepository(repositoryDbContext));
        _lazyUnitOfWork = new Lazy<IUnitOfWork>(() => new UnitOfWork(repositoryDbContext));
    }


    public IBookingRepository BookingRepository => _lazyBookingRepository.Value;
    public ICircuiteRepository CircuiteRepository => _lazyCircuitRepository.Value;
    public IConstructorRepository ConstructorRepository => _lazyConstructorRepository.Value;
    public ICountryRepository CountryRepository => _lazyCountryRepository.Value;
    public IDriverRepository DriverRepository => _lazyDriverRepository.Value;
    public IMediaTagRepository MediaTagRepository => _lazyMediaTagRepository.Value;
    public IPositionRepository PositionRepository => _lazyPositionRepository.Value;
    public IQualificationPeriodsRepository QualificationPeriodsRepository => _lazyQualificationPeriodsRepository.Value;
    public IRaceClassificationRepository RaceClassificationRepository => _lazyRaceClassificationRepository.Value;
    public IRaceFastesLapRepository RaceFastesLapRepository => _lazyFastestLapRepository.Value;
    public IRaceSpeedTrapRepository RaceSpeedTrapRepository => _lazyRaceSpeedTrapRepository.Value;
    public IRacePitStop RacePitStop => _lazyPitStopRepository.Value;
    public IRaceweekRepository RaceweekRepository => _lazyRaceweekRepository.Value;
    public ISeasonRepository SeasonRepository => _lazySeasonRepository.Value;
    public ISectorRepository SectorRepository => _lazySectorRepository.Value;
    public IRaceBestSectorsRepository BestSectorsRepository => _lazyRaceBestSectorsRepository.Value;
    public IRaceMaximumSpeedsRepository RaceMaximumSpeedsRepository => _lazyRaceMaximumSpeedsRepository.Value;

    public IQualificationClassificationRepository QualificationClassificationRepository =>
        _lazyQualificationClassificationRepository.Value;

    public IQualificationBestSectorTimesRepository QualificationBestSectorTimesRepository =>
        _lazyQualificationBestSectorTimesRepository.Value;

    public IQualificationMaximumSpeedRepository QualificationMaximumSpeedRepository =>
        _lazyQualificationMaximumSpeedRepository.Value;

    public IQualificationSpeedTrapRepository QualificationSpeedTrapRepository =>
        _lazyQualificationSpeedTrapRepository.Value;
    public IUnitOfWork UnitOfWork => _lazyUnitOfWork.Value;
}