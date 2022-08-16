using FormulaCar.Championships.Domain.Entities;

namespace FormulaCar.Championships.Domain.Repositories;

public interface ICountryRepository : IBaseRepository<Country>
{
    Task<IEnumerable<Driver>> GetDrivers(int countryId);
    Task<IEnumerable<Constructor>> GetConstructors(int countryId);
    Task<IEnumerable<Circuite>> GetCircuites(int countryId);
    Task<IEnumerable<Country>> GetAllWithMedia();
    Task<bool> InsertCountry(Country country, int mediaTagId);
}