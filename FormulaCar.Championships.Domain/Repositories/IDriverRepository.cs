using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormulaCar.Championships.Domain.Entities;

namespace FormulaCar.Championships.Domain.Repositories
{
    public interface IDriverRepository:IBaseRepository<Driver>
    {
        bool Exist(string firstName, string lastName);
    }
}
