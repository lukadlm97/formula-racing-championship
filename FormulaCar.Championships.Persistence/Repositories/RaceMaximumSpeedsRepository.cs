using FormulaCar.Championships.Domain.Entities;
using FormulaCar.Championships.Domain.Repositories;

namespace FormulaCar.Championships.Persistence.Repositories;

public class RaceMaximumSpeedsRepository : RepositoryBase<RaceMaximumSpeed>, IRaceMaximumSpeedsRepository
{
    public RaceMaximumSpeedsRepository(RepositoryDbContext repositoryDbContext):base(repositoryDbContext)
    {
        
    }
}