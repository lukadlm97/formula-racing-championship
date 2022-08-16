using FormulaCar.Championships.Domain.Entities;

namespace FormulaCar.Championships.Domain.Repositories;

public interface ICircuiteRepository : IBaseRepository<Circuite>
{
    Task InsertCircuite(Circuite circuite);
}