using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FormulaCar.Championships.Domain.Repositories
{
    public interface IBaseRepository<T>
    where T : class
    {
        Task<IQueryable<T>> FindAll(CancellationToken cancellationToken=default);
        Task<IQueryable<T>> FindByCondition(Expression<Func<T, bool>> expression,CancellationToken cancellationToken= default);
        void Insert(T entity);
        void Remove(T entity);
        void Update(T entity);
    }
}
