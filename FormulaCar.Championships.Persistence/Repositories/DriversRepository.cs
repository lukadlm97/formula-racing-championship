using FormulaCar.Championships.Domain.Entities;
using FormulaCar.Championships.Domain.Repositories;

namespace FormulaCar.Championships.Persistence.Repositories;

public class DriversRepository : RepositoryBase<Driver>, IDriverRepository
{
    public DriversRepository(RepositoryDbContext repositoryContext) : base(repositoryContext)
    {
    }

    public bool Exist(string firstName, string lastName)
    {
        return RepositoryContext.Drivers.Any(x =>
            x.FirstName.ToLower().Contains(firstName.ToLower()) && x.LastName.ToLower().Contains(lastName.ToLower()));
    }
}