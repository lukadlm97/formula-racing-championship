using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormulaCar.Championships.Contracts;

namespace FormulaCar.Championships.Importers.Fetchers
{
    public interface IQualificationSpeedTrapFetcher
    {
        Task<IEnumerable<QualificationSpeedTrapForCreationDto>> Get(string grandPrix, string season);
    }
}
