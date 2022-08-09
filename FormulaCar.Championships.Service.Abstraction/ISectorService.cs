using FormulaCar.Championships.Contracts;

namespace FormulaCar.Championships.Service.Abstraction;

public interface ISectorService
{
    Task<IEnumerable<SectorDto>> GetAll(CancellationToken cancellationToken=default);
}