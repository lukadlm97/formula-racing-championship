using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormulaCar.Championships.Contracts;

namespace FormulaCar.Championships.Service.Abstraction
{
    public interface IFastestLapService
    {
        Task<IEnumerable<RaceFastestLapDto>> GetAll();
        Task<bool> Exist(string driver, string circuit, string season, string position);
        Task<RaceFastestLapDto> Create(RaceFastestLapForCreationDto raceFastestLapDto);
    }
}
