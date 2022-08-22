using FormulaCar.Championships.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaCar.Championships.Service.Abstraction
{
    public interface IRacePitStopService
    {
        Task<IEnumerable<RacePitStopDto>> GetAll();
        Task<bool> Exist(string driver, string circuit, string season, string position);
        Task<RacePitStopDto> Create(RacePitStopForCreation raceFastestLapDto);
    }
}
