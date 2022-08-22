using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormulaCar.Championships.Contracts;

namespace FormulaCar.Championships.Importers.Fetchers
{
    public interface IQualificationClassificationFetcher
    {
        Task<IEnumerable<QualificationClassificationForCreationDto>> Get(string grandPrix, string season);
    }
}
