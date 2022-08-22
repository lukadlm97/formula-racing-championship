using FormulaCar.Championships.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaCar.Championships.Service.Abstraction
{
    public interface IRaceBestSectorService
    {
        Task<IEnumerable<RaceBestSectorTimesDto>> GetAll();
        Task<RaceBestSectorTimesDto> Create(RaceBestSectorTimesForCreationDto raceResultItemForCreationDto);
        Task<bool> Exist(string driver, string circuit, string season, string position,int sector);
    }
}
