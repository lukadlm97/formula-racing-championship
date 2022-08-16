using AutoMapper;
using FormulaCar.Championships.Contracts;
using FormulaCar.Championships.Domain.Repositories;
using FormulaCar.Championships.Service.Abstraction;

namespace FormulaCar.Championships.Service;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<ICircuiteService> _lazyCircuiteService;
    private readonly Lazy<ICountryService> _lazyCountryService;
    private readonly Lazy<IDriverService> _lazyDriverService;
    private readonly Lazy<IPositionService> _lazyPositionService;
    private readonly Lazy<IQualificationPeriodsService> _lazyQualificationPeriodsService;
    private readonly Lazy<ISectorService> _lazySectorService;
    private readonly Lazy<IConstructorService> _lazyConstructorService;
    private readonly Lazy<IBookingService> _lazyBookingService;
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
        _lazyConstructorService = new Lazy<IConstructorService>(() => new ConstructorService(repositoryManager, _mapper));
        _lazyBookingService = new Lazy<IBookingService>(() => new BookingService(repositoryManager, _mapper));
    }

    public IPositionService PositionService => _lazyPositionService.Value;
    public ISectorService SectorService => _lazySectorService.Value;
    public IQualificationPeriodsService QualificationPeriodsService => _lazyQualificationPeriodsService.Value;
    public ICountryService CountryService => _lazyCountryService.Value;
    public ICircuiteService CircuiteService => _lazyCircuiteService.Value;
    public IDriverService DriverService => _lazyDriverService.Value;
    public IConstructorService ConstructorService => _lazyConstructorService.Value;
    public IBookingService BookingService => _lazyBookingService.Value;
}