using FormulaCar.Championships.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaCar.Championships.Service.Abstraction
{
    public interface IQualificationSpeedTrapService
    {
        Task<IEnumerable<QualificationSpeedTrapDto>> GetAll();

        Task<QualificationSpeedTrapDto> Create(
            QualificationSpeedTrapForCreationDto raceResultItemForCreationDto);

        Task<bool> Exist(string driver, string circuit, string season, string position, int sector);
    }
}
