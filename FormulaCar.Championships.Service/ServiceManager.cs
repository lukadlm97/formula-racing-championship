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
        private readonly IMapper _mapper;

        public ServiceManager(IRepositoryManager repositoryManager,IMapper mapper)
        {
            _mapper = mapper;
            _lazyPositionService = new Lazy<IPositionService>(() => new PositionService(repositoryManager, _mapper));
        }

        public IPositionService PositionService => _lazyPositionService.Value;
    }
}
