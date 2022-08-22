using FormulaCar.Championships.Domain.Entities;
using FormulaCar.Championships.Domain.Repositories;

namespace FormulaCar.Championships.Persistence.Repositories;

public class RaceBestSectorRepository : RepositoryBase<RaceSectorTime>, IRaceBestSectorsRepository
{
    public RaceBestSectorRepository(RepositoryDbContext repositoryDbContext) : base(repositoryDbContext)
    {
    }
}