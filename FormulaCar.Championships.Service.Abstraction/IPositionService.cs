using FormulaCar.Championships.Contracts;

namespace FormulaCar.Championships.Service.Abstraction;

public interface IPositionService
{
    Task<PositionDto> Create(PositionForCreationDto positionDto,CancellationToken cancellationToken=default);
    Task<PositionDto> Update(PositionForUpdateDto positionDto, CancellationToken cancellationToken = default);
    Task<bool> Delete(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<PositionDto>> GetAll( CancellationToken cancellationToken = default);
    Task<PositionDto> GetById(int id, CancellationToken cancellationToken = default);
}

