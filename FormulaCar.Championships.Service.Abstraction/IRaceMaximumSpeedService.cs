using FormulaCar.Championships.Contracts;

namespace FormulaCar.Championships.Service.Abstraction;

public interface IRaceMaximumSpeedService
{
    Task<bool> Exist(string driver, string circuit, string season, string position, int sector);
    Task<RaceMaximumSpeedDto> Create(RaceMaximumSpeedForCreationDto raceMaximumSpeedForCreationDto);
    Task<IEnumerable<RaceMaximumSpeedDto>> GetAll();
}