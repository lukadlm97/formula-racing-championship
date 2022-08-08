using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormulaCar.Championships.Domain.Repositories;
using FormulaCar.Championships.Service.Abstraction;

namespace FormulaCar.Championships.Service
{
    public class ServiceManager:IServiceManager
    {
        private readonly Lazy<IPositionService> _lazyPositionService;

        public ServiceManager(IRepositoryManager repositoryManager)
        {
            _lazyPositionService = new Lazy<IPositionService>(() => new PositionService(repositoryManager));
        }

        public IPositionService PositionService => _lazyPositionService.Value;
    }
}
