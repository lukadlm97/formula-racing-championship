
using AutoMapper;
using FormulaCar.Championships.Contracts;
using FormulaCar.Championships.Domain.Entities;
using FormulaCar.Championships.Domain.Repositories;
using FormulaCar.Championships.Service.Abstraction;

namespace FormulaCar.Championships.Service
{
    public class CircuiteService:ICircuiteService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public CircuiteService(IRepositoryManager repositoryManager,IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper=mapper;
        }
        public async Task<IEnumerable<CircuitDto>> GetAll(CancellationToken cancellationToken = default)
        {
            var circuites = await _repositoryManager.CircuiteRepository.FindAll(cancellationToken);
            var countries = await _repositoryManager.CountryRepository.FindAll(cancellationToken);

            List<CircuitDto> circuitDtos = new List<CircuitDto>();
            foreach (var circuite in circuites)
            {
                var castedCircuite = _mapper.Map<CircuitDto>(circuite);
                castedCircuite.CountryCode = countries.FirstOrDefault(x=>x.Id==circuite.CountryId)?.Code;
                circuitDtos.Add(castedCircuite);
            }
            return circuitDtos;
        }

        public async Task<CircuitDto> Create(CircuitForCreationDto circuitForCreate, CancellationToken cancellationToken = default)
        {
            var circuit = _mapper.Map<Circuite>(circuitForCreate);
            await _repositoryManager.CircuiteRepository.InsertCircuite(circuit);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
            var createdObj = _mapper.Map<CircuitDto>(circuit);
            return createdObj;
        }
    }
}
