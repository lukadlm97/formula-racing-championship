using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormulaCar.Championships.Domain.Entities;

namespace FormulaCar.Championships.Domain.Repositories
{
    public interface ICircuiteRepository:IBaseRepository<Circuite>
    {
        Task InsertCircuite(Circuite circuite);
    }
}
