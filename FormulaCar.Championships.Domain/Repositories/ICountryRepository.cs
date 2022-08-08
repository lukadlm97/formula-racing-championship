using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormulaCar.Championships.Domain.Entities;

namespace FormulaCar.Championships.Domain.Repositories
{
    public interface ICountryRepository:IBaseRepository<Country>
    {
        Task<IEnumerable<Driver>> GetDrivers(int countryId);
        Task<IEnumerable<Constructor>> GetConstructors(int countryId);
        Task<IEnumerable<Circuite>> GetCircuites(int countryId);
    }
}
