﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormulaCar.Championships.Contracts;

namespace FormulaCar.Championships.Importers.Fetchers
{
    public interface IRaceFetcher
    {
        Task<IEnumerable<RaceResultItemForCreationDto>> GetRaceResults(string grandPrix,int season);
    }
}
