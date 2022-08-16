using System.Linq.Expressions;

namespace FormulaCar.Championships.Domain.Repositories;

public interface IBaseRepository<T>
    where T : class
{
    Task<IQueryable<T>> FindAll(CancellationToken cancellationToken = default);

    Task<IQueryable<T>> FindByCondition(Expression<Func<T, bool>> expression,
        CancellationToken cancellationToken = default);

    void Insert(T entity);
    void Remove(T entity);
    void Update(T entity);
}