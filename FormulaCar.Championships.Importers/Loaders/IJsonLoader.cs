using FormulaCar.Championships.Contracts;

namespace FormulaCar.Championships.Importers.Loaders;

public interface IJsonLoader
{
    Task<IEnumerable<CountryDto>> GetCountries(string jsonPath);
}