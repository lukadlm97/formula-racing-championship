using FormulaCar.Championships.Domain.Entities;
using FormulaCar.Championships.Domain.Repositories;

namespace FormulaCar.Championships.Persistence.Repositories;

public class PositionRepository : RepositoryBase<Position>, IPositionRepository
{
    public PositionRepository(RepositoryDbContext repositoryContext) : base(repositoryContext)
    {
    }
}