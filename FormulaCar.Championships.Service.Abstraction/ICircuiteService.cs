using FormulaCar.Championships.Contracts;

namespace FormulaCar.Championships.Service.Abstraction;

public interface ICircuiteService
{
    Task<IEnumerable<CircuitDto>> GetAll(CancellationToken cancellationToken = default);
    Task<CircuitDto> Create(CircuitForCreationDto circuitForCreate, CancellationToken cancellationToken = default);
}