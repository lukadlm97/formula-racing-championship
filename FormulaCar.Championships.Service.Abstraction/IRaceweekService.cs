using FormulaCar.Championships.Contracts;

namespace FormulaCar.Championships.Service.Abstraction;

public interface IRaceweekService
{
    Task<IEnumerable<GrandPrixDto>> GetAll();
    Task<GrandPrixDto> Create(GrandPrixForCreation grandPrixForCreation);
    Task<bool> Exist(string grandPrix, string season);
}