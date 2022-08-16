using FormulaCar.Championships.Contracts;

namespace FormulaCar.Championships.Service.Abstraction;

public interface IDriverService
{
    Task<IEnumerable<DriverDto>> GetDrivers();
    Task<DriverDto> Create(DriverForCreationDto driver);
    Task<bool> Exist(string firstName, string lastName);
}