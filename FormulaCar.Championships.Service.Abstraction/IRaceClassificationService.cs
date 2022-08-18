using FormulaCar.Championships.Contracts;

namespace FormulaCar.Championships.Service.Abstraction;

public interface IRaceClassificationService
{
    Task<IEnumerable<RaceResultItemDto>> GetAll();
    Task<RaceResultItemDto> Create(RaceResultItemForCreationDto raceResultItemForCreationDto);
    Task<IEnumerable<RaceResultItemDto>> GetByRace(int raceweekId);
    Task<bool> Exist(string driver, string circuit, string season,string position);
}