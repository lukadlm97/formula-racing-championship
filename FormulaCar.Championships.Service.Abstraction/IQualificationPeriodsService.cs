using FormulaCar.Championships.Contracts;

namespace FormulaCar.Championships.Service.Abstraction;

public interface IQualificationPeriodsService
{
    Task<IEnumerable<QualificationPeriodsDto>> GetAll(CancellationToken cancellationToken = default);
}