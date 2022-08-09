using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormulaCar.Championships.Contracts;
using FormulaCar.Championships.Domain.Entities;
using FormulaCar.Championships.Domain.Exceptions;
using FormulaCar.Championships.Domain.Repositories;
using FormulaCar.Championships.Service.Abstraction;
using IMapper = AutoMapper.IMapper;

namespace FormulaCar.Championships.Service
{
    public class SectorService : ISectorService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public SectorService(IRepositoryManager repositoryManager,IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }
        public async Task<IEnumerable<SectorDto>> GetAll(CancellationToken cancellationToken=default)
        {
            var sectors = await _repositoryManager.SectorRepository.FindAll();

            if (!sectors.Any())
            {
                throw new ItemsNotFoundException(nameof(Sector));
            }

            List<SectorDto> sectorDtos = new List<SectorDto>();
            foreach (var sector in sectors)
            {
                sectorDtos.Add(_mapper.Map<SectorDto>(sector));
            }

            return sectorDtos;
        }
    }
}
