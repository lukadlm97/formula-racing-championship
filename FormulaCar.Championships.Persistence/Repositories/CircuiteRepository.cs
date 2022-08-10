using FormulaCar.Championships.Domain.Entities;
using FormulaCar.Championships.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FormulaCar.Championships.Persistence.Repositories;

public class CircuiteRepository : RepositoryBase<Circuite>, ICircuiteRepository
{
    public CircuiteRepository(RepositoryDbContext repositoryContext) : base(repositoryContext)
    {
    }

    public async Task InsertCircuite(Circuite circuite)
    {
        var country = await RepositoryContext.Countries.FirstOrDefaultAsync(x => x.Id == circuite.CountryId);

        circuite.CountryId = country.Id;

        await RepositoryContext.AddAsync(circuite);
    }
}