using FormulaCar.Championships.Domain.Entities;
using FormulaCar.Championships.Domain.Repositories;

namespace FormulaCar.Championships.Persistence.Repositories;

public class CircuiteRepository : RepositoryBase<Circuite>, ICircuiteRepository
{
    public CircuiteRepository(RepositoryDbContext repositoryContext) : base(repositoryContext)
    {
    }
}