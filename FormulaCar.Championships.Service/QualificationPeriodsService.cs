using AutoMapper;
using FormulaCar.Championships.Contracts;
using FormulaCar.Championships.Domain.Repositories;
using FormulaCar.Championships.Service.Abstraction;

namespace FormulaCar.Championships.Service;

public class QualificationPeriodsService : IQualificationPeriodsService
{
    private readonly IMapper _mapper;
    private readonly IRepositoryManager _repositoryManager;

    public QualificationPeriodsService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public async Task<IEnumerable<QualificationPeriodsDto>> GetAll(CancellationToken cancellationToken = default)
    {
        var qualificationPeriods =
            await _repositoryManager.QualificationPeriodsRepository.FindAll(cancellationToken);

        var qualificationPeriodsDto = new List<QualificationPeriodsDto>();
        foreach (var qualificationPeriod in qualificationPeriods)
            qualificationPeriodsDto.Add(_mapper.Map<QualificationPeriodsDto>(qualificationPeriod));

        return qualificationPeriodsDto;
    }
}