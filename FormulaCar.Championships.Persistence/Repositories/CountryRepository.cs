using FormulaCar.Championships.Domain.Entities;
using FormulaCar.Championships.Domain.Repositories;

namespace FormulaCar.Championships.Persistence.Repositories;

public class CountryRepository : RepositoryBase<Country>, ICountryRepository
{
    public CountryRepository(RepositoryDbContext repositoryContext) : base(repositoryContext)
    {
    }

    public Task<IEnumerable<Driver>> GetDrivers(int countryId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Constructor>> GetConstructors(int countryId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Circuite>> GetCircuites(int countryId)
    {
        throw new NotImplementedException();
    }
}