using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormulaCar.Championships.Contracts;

namespace FormulaCar.Championships.Service.Abstraction
{
    public interface IConstructorService
    {
        Task<ConstructorDto> CreateConstructor(ConstructorForCreationDto constructorForCreationDto,CancellationToken cancellationToken=default);
        Task<IEnumerable<ConstructorDto>> GetConstructors(CancellationToken cancellationToken = default);
        Task<bool> Exist(string name, CancellationToken cancellationToken = default);
    }
}
