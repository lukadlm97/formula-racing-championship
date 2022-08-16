using FormulaCar.Championships.Contracts;

namespace FormulaCar.Championships.Importers.Fetchers;

public interface ICircuitFetcher
{
    Task<IEnumerable<CircuitDto>> GetCircuites();
}