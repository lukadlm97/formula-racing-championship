using FormulaCar.Championships.Domain.Entities;
using FormulaCar.Championships.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

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

    public async Task<IEnumerable<Country>> GetAllWithMedia()
    {
        return await RepositoryContext.Countries.Include(x => x.MediaTag).ToListAsync();
    }
    
    public async Task<bool> InsertCountry(Country country, int mediaTagId)
    {
        var mediaTag = await RepositoryContext.MediaTags.FirstOrDefaultAsync(x => x.Id == mediaTagId);
        country.MediaTag= mediaTag;

        await RepositoryContext.Countries.AddAsync(country);

        return true;
    }
}