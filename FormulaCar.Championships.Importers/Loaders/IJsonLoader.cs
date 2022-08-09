using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormulaCar.Championships.Contracts;
using FormulaCar.Championships.Domain.Entities;

namespace FormulaCar.Championships.Importers.Loaders
{
    public interface IJsonLoader
    {
        Task<IEnumerable<CountryDto>> GetCountries(string jsonPath);
    }
}
