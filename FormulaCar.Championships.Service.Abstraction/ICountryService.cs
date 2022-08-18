using FormulaCar.Championships.Contracts;

namespace FormulaCar.Championships.Service.Abstraction;

public interface ICountryService
{
    Task<IEnumerable<CountryDto>> GetCountries(CancellationToken cancellationToken);
    Task<CountryDto> Create(CountryForCreationDto countryForCreationDto, CancellationToken cancellationToken);
    Task<int> GetIdByCode(string code);
    Task<int> GetIdByName(string name);
    Task<string> GetCodeById(int id);
}