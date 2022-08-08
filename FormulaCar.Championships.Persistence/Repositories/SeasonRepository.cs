using FormulaCar.Championships.Domain.Entities;
using FormulaCar.Championships.Domain.Repositories;

namespace FormulaCar.Championships.Persistence.Repositories;

public class SeasonRepository : RepositoryBase<Season>, ISeasonRepository
{
    public SeasonRepository(RepositoryDbContext repositoryContext) : base(repositoryContext)
    {
    }
}