using FormulaCar.Championships.Domain.Entities;
using FormulaCar.Championships.Domain.Repositories;

namespace FormulaCar.Championships.Persistence.Repositories;

public class ConstructorRepository : RepositoryBase<Constructor>, IConstructorRepository
{
    public ConstructorRepository(RepositoryDbContext repositoryContext) : base(repositoryContext)
    {
    }
}