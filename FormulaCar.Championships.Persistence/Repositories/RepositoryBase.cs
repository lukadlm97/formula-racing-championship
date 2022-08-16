using System.Linq.Expressions;
using FormulaCar.Championships.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FormulaCar.Championships.Persistence.Repositories;

public abstract class RepositoryBase<T> : IBaseRepository<T> where T : class
{
    public RepositoryBase(RepositoryDbContext repositoryContext)
    {
        RepositoryContext = repositoryContext;
    }

    protected RepositoryDbContext RepositoryContext { get; set; }

    public async Task<IQueryable<T>> FindAll(CancellationToken cancellationToken = default)
    {
        return RepositoryContext.Set<T>().AsNoTracking();
    }

    public async Task<IQueryable<T>> FindByCondition(Expression<Func<T, bool>> expression,
        CancellationToken cancellationToken = default)
    {
        return RepositoryContext.Set<T>()
            .Where(expression).AsNoTracking();
    }

    public void Insert(T entity)
    {
        RepositoryContext.Set<T>().Add(entity);
    }

    public void Remove(T entity)
    {
        RepositoryContext.Set<T>().Remove(entity);
    }

    public void Update(T entity)
    {
        RepositoryContext.Set<T>().Update(entity);
    }
}