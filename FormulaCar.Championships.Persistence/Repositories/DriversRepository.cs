using FormulaCar.Championships.Domain.Entities;
using FormulaCar.Championships.Domain.Repositories;

namespace FormulaCar.Championships.Persistence.Repositories;

public class DriversRepository : RepositoryBase<Driver>, IDriverRepository
{
    public DriversRepository(RepositoryDbContext repositoryContext) : base(repositoryContext)
    {
    }
}