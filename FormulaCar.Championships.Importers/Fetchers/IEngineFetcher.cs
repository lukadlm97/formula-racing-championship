using FormulaCar.Championships.Contracts;

namespace FormulaCar.Championships.Importers.Fetchers;

public interface IEngineFetcher
{
    Task<IEnumerable<EngineForCreationDto>> GetEngines();
}