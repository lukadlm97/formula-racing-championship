using FormulaCar.Championships.Domain.Entities;
using FormulaCar.Championships.Domain.Repositories;

namespace FormulaCar.Championships.Persistence.Repositories;

public class RaceweekRepository : RepositoryBase<Raceweek>, IRaceweekRepository
{
    public RaceweekRepository(RepositoryDbContext repositoryContext) : base(repositoryContext)
    {
    }
}