using FormulaCar.Championships.Domain.Entities;
using FormulaCar.Championships.Domain.Repositories;

namespace FormulaCar.Championships.Persistence.Repositories;

public class SectorsRepository : RepositoryBase<Sector>, ISectorRepository
{
    public SectorsRepository(RepositoryDbContext repositoryContext) : base(repositoryContext)
    {
    }
}