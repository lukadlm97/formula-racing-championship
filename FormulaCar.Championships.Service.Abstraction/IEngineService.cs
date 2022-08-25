using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormulaCar.Championships.Contracts;

namespace FormulaCar.Championships.Service.Abstraction
{
    public interface IEngineService
    {
        Task<IEnumerable<EngineDto>> GetAll();
        Task<EngineDto> Create(EngineForCreationDto engineForCreationDto);
        Task<bool> Exist(string name);
    }
}
