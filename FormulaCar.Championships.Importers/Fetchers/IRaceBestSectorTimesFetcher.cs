using FormulaCar.Championships.Contracts;

namespace FormulaCar.Championships.Importers.Fetchers;

public interface IRaceBestSectorTimesFetcher
{
    Task<IEnumerable<RaceBestSectorTimesForCreationDto>> GetBestSectorTimes(string grandPrix, string season);
}