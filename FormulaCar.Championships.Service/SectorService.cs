using AutoMapper;
using FormulaCar.Championships.Contracts;
using FormulaCar.Championships.Domain.Entities;
using FormulaCar.Championships.Domain.Exceptions;
using FormulaCar.Championships.Domain.Repositories;
using FormulaCar.Championships.Service.Abstraction;

namespace FormulaCar.Championships.Service;

public class SectorService : ISectorService
{
    private readonly IMapper _mapper;
    private readonly IRepositoryManager _repositoryManager;

    public SectorService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SectorDto>> GetAll(CancellationToken cancellationToken = default)
    {
        var sectors = await _repositoryManager.SectorRepository.FindAll();

        if (!sectors.Any()) throw new ItemsNotFoundException(nameof(Sector));

        var sectorDtos = new List<SectorDto>();
        foreach (var sector in sectors) sectorDtos.Add(_mapper.Map<SectorDto>(sector));

        return sectorDtos;
    }
}