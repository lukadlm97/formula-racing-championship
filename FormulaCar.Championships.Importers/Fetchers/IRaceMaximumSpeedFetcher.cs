using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormulaCar.Championships.Contracts;

namespace FormulaCar.Championships.Importers.Fetchers
{
    public interface IRaceMaximumSpeedFetcher
    {
        Task<IEnumerable<RaceMaximumSpeedForCreationDto>> GetRaceMaximumSpeeds(string grandPrix, string season);
    }
}
