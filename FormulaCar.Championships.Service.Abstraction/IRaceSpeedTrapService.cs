using FormulaCar.Championships.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaCar.Championships.Service.Abstraction
{
    public interface IRaceSpeedTrapService
    {
        Task<IEnumerable<RaceSpeedTrapDto>> GetAll();
        Task<bool> Exist(string driver, string circuit, string season, string position);
        Task<RaceSpeedTrapDto> Create(RaceSpeedTrapForCreation raceFastestLapDto);
    }
}
