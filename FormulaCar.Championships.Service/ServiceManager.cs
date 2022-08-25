using AutoMapper;
using FormulaCar.Championships.Domain.Repositories;
using FormulaCar.Championships.Service.Abstraction;

namespace FormulaCar.Championships.Service;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IBookingService> _lazyBookingService;
    private readonly Lazy<ICircuiteService> _lazyCircuiteService;
    private readonly Lazy<IConstructorService> _lazyConstructorService;
    private readonly Lazy<ICountryService> _lazyCountryService;
    private readonly Lazy<IDriverService> _lazyDriverService;
    private readonly Lazy<IFastestLapService> _lazyFastestLapService;
    private readonly Lazy<IPositionService> _lazyPositionService;
    private readonly Lazy<IQualificationPeriodsService> _lazyQualificationPeriodsService;
    private readonly Lazy<IRaceBestSectorService> _lazyRaceBestSectorService;
    private readonly Lazy<IRaceClassificationService> _lazyRaceClassificationService;
    private readonly Lazy<IRaceMaximumSpeedService> _lazyRaceMaximumSpeedService;
    private readonly Lazy<IRacePitStopService> _lazyRacePitStopService;
    private readonly Lazy<IRaceSpeedTrapService> _lazyRaceSpeedTrapService;
    private readonly Lazy<IRaceweekService> _lazyRaceweekService;
    private readonly Lazy<ISectorService> _lazySectorService;
    private readonly Lazy<IQualificationMaxSpeedService> _lazyQualificationMaxSpeedService;
    private readonly Lazy<IQualificationSpeedTrapService> _lazyQualificationSpeedTrapService;
    private readonly Lazy<IQualificationClassificationService> _lazyQualificationClassificationService;
    private readonly Lazy<IQualificationBestSectorService> _lazyQualificationBestSectorService;
    private readonly Lazy<IEngineService> _lazyEngineService;
    private readonly IMapper _mapper;

    public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _mapper = mapper;
        _lazyPositionService = new Lazy<IPositionService>(() => new PositionService(repositoryManager, _mapper));
        _lazySectorService = new Lazy<ISectorService>(() => new SectorService(repositoryManager, _mapper));
        _lazyQualificationPeriodsService =
            new Lazy<IQualificationPeriodsService>(() => new QualificationPeriodsService(repositoryManager, _mapper));
        _lazyCountryService = new Lazy<ICountryService>(() => new CountryService(repositoryManager, _mapper));
        _lazyCircuiteService = new Lazy<ICircuiteService>(() => new CircuiteService(repositoryManager, _mapper));
        _lazyDriverService = new Lazy<IDriverService>(() => new DriverService(repositoryManager, _mapper));
        _lazyConstructorService =
            new Lazy<IConstructorService>(() => new ConstructorService(repositoryManager, _mapper));
        _lazyBookingService = new Lazy<IBookingService>(() => new BookingService(repositoryManager, _mapper));
        _lazyRaceweekService = new Lazy<IRaceweekService>(() => new RaceweekService(repositoryManager, _mapper));
        _lazyRaceClassificationService =
            new Lazy<IRaceClassificationService>(() => new RaceClassificationService(repositoryManager, _mapper));
        _lazyFastestLapService =
            new Lazy<IFastestLapService>(() => new RaceFastestLapService(repositoryManager, _mapper));
        _lazyRacePitStopService =
            new Lazy<IRacePitStopService>(() => new RacePitStopService(repositoryManager, _mapper));
        _lazyRaceSpeedTrapService =
            new Lazy<IRaceSpeedTrapService>(() => new RaceSpeedTrapService(repositoryManager, _mapper));
        _lazyRaceBestSectorService =
            new Lazy<IRaceBestSectorService>(() => new RaceBestSectorService(repositoryManager, _mapper));
        _lazyRaceMaximumSpeedService =
            new Lazy<IRaceMaximumSpeedService>(() => new RaceMaximumSpeedService(repositoryManager, _mapper)); 
        _lazyQualificationSpeedTrapService =
            new Lazy<IQualificationSpeedTrapService>(() => new QualificationSpeedTrapService(repositoryManager, _mapper));
        _lazyQualificationMaxSpeedService =
            new Lazy<IQualificationMaxSpeedService>(() => new QualificationMaxSpeedService(repositoryManager, _mapper));
        _lazyQualificationBestSectorService =
            new Lazy<IQualificationBestSectorService>(() => new QualificationBestSectorTimeService(repositoryManager, _mapper));
        _lazyQualificationClassificationService =
            new Lazy<IQualificationClassificationService>(() => new QualificationClassificationService(repositoryManager, _mapper));
        _lazyEngineService =
            new Lazy<IEngineService>(() => new EngineService(repositoryManager, _mapper));

    }

    public IPositionService PositionService => _lazyPositionService.Value;
    public ISectorService SectorService => _lazySectorService.Value;
    public IQualificationPeriodsService QualificationPeriodsService => _lazyQualificationPeriodsService.Value;
    public ICountryService CountryService => _lazyCountryService.Value;
    public ICircuiteService CircuiteService => _lazyCircuiteService.Value;
    public IDriverService DriverService => _lazyDriverService.Value;
    public IConstructorService ConstructorService => _lazyConstructorService.Value;
    public IBookingService BookingService => _lazyBookingService.Value;
    public IRaceweekService RaceweekService => _lazyRaceweekService.Value;
    public IRaceClassificationService RaceClassificationService => _lazyRaceClassificationService.Value;
    public IFastestLapService FastestLapService => _lazyFastestLapService.Value;
    public IRaceSpeedTrapService RaceSpeedTrapService => _lazyRaceSpeedTrapService.Value;
    public IRacePitStopService RacePitStopService => _lazyRacePitStopService.Value;
    public IRaceBestSectorService RaceBestSectorService => _lazyRaceBestSectorService.Value;
    public IRaceMaximumSpeedService RaceMaximumSpeedService => _lazyRaceMaximumSpeedService.Value;
    public IQualificationBestSectorService QualificationBestSectorService => _lazyQualificationBestSectorService.Value;

    public IQualificationClassificationService QualificationClassificationService =>
        _lazyQualificationClassificationService.Value;

    public IQualificationMaxSpeedService QualificationMaxSpeedService => _lazyQualificationMaxSpeedService.Value;
    public IQualificationSpeedTrapService QualificationSpeedTrapService => _lazyQualificationSpeedTrapService.Value;
    public IEngineService EngineService => _lazyEngineService.Value;
}