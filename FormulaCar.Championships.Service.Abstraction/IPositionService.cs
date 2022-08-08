using FormulaCar.Championships.Contracts;

namespace FormulaCar.Championships.Service.Abstraction;

public interface IPositionService
{
    Task<PositionDto> Create(PositionForCreationDto positionDto);
    Task<PositionDto> Update(PositionForUpdateDto positionDto);
    Task<bool> Delete(int id);
    Task<IEnumerable<PositionDto>> GetAll();
}

