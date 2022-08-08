﻿using FormulaCar.Championships.Domain.Repositories;

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
        _lazyUnitOfWork = new Lazy<IUnitOfWork>(() => new UnitOfWork(repositoryDbContext));
    }


    public IBookingRepository BookingRepository => _lazyBookingRepository.Value;
    public ICircuiteRepository CircuiteRepository => _lazyCircuitRepository.Value;
    public IConstructorRepository ConstructorRepository => _lazyConstructorRepository.Value;
    public ICountryRepository CountryRepository => _lazyCountryRepository.Value;
    public IDriverRepository DriverRepository => _lazyDriverRepository.Value;
    public IMediaTagRepository MediaTagRepository => _lazyMediaTagRepository.Value;
    public IPositionRepository PositionRepository => _lazyPositionRepository.Value;
    public IQualificationPeriodsRepository QualificationPeriodsRepository { get; }
    public IRaceClassificationRepository RaceClassificationRepository => _lazyRaceClassificationRepository.Value;
    public IRaceweekRepository RaceweekRepository => _lazyRaceweekRepository.Value;
    public ISeasonRepository SeasonRepository => _lazySeasonRepository.Value;
    public ISectorRepository SectorRepository => _lazySectorRepository.Value;
    public IUnitOfWork UnitOfWork => _lazyUnitOfWork.Value;
}