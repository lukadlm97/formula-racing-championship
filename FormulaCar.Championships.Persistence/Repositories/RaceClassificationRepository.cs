using FormulaCar.Championships.Domain.Entities;
using FormulaCar.Championships.Domain.Repositories;

namespace FormulaCar.Championships.Persistence.Repositories;

public class RaceClassificationRepository : RepositoryBase<RaceClassification>, IRaceClassificationRepository
{
    public RaceClassificationRepository(RepositoryDbContext repositoryContext) : base(repositoryContext)
    {
    }
}