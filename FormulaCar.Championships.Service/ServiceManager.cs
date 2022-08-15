using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FormulaCar.Championships.Domain.Repositories;
using FormulaCar.Championships.Service.Abstraction;

namespace FormulaCar.Championships.Service
{
    public class ServiceManager:IServiceManager
    {
        private readonly Lazy<IPositionService> _lazyPositionService;
        private readonly Lazy<ISectorService> _lazySectorService;
        private readonly Lazy<IQualificationPeriodsService> _lazyQualificationPeriodsService;
        private readonly Lazy<ICountryService> _lazyCountryService;
        private readonly Lazy<ICircuiteService> _lazyCircuiteService;
        private readonly Lazy<IDriverService> _lazyDriverService;
        private readonly IMapper _mapper;

        public ServiceManager(IRepositoryManager repositoryManager,IMapper mapper)
        {
            _mapper = mapper;
            _lazyPositionService = new Lazy<IPositionService>(() => new PositionService(repositoryManager, _mapper));
            _lazySectorService = new Lazy<ISectorService>(() => new SectorService(repositoryManager, _mapper));
            _lazyQualificationPeriodsService = new Lazy<IQualificationPeriodsService>(() => new QualificationPeriodsService(repositoryManager, _mapper));
            _lazyCountryService = new Lazy<ICountryService>(() => new CountryService(repositoryManager, _mapper));
            _lazyCircuiteService = new Lazy<ICircuiteService>(() => new CircuiteService(repositoryManager, _mapper));
            _lazyDriverService = new Lazy<IDriverService>(() => new DriverService(repositoryManager, _mapper));
        }

        public IPositionService PositionService => _lazyPositionService.Value;
        public ISectorService SectorService => _lazySectorService.Value;
        public IQualificationPeriodsService QualificationPeriodsService => _lazyQualificationPeriodsService.Value;
        public ICountryService CountryService => _lazyCountryService.Value;
        public ICircuiteService CircuiteService => _lazyCircuiteService.Value;
        public IDriverService DriverService => _lazyDriverService.Value;
    }
}
