using FormulaCar.Championships.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaCar.Championships.Service.Abstraction
{
    public interface IQualificationMaxSpeedService
    {
        Task<IEnumerable<QualificationMaximumSpeedDto>> GetAll();

        Task<QualificationMaximumSpeedDto> Create(
            QualificationMaximumSpeedForCreationDto raceResultItemForCreationDto);

        Task<bool> Exist(string driver, string circuit, string season, string position, int sector);

    }
}
