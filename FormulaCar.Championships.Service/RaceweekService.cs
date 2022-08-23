using AutoMapper;
using FormulaCar.Championships.Contracts;
using FormulaCar.Championships.Domain.Entities;
using FormulaCar.Championships.Domain.Exceptions;
using FormulaCar.Championships.Domain.Repositories;
using FormulaCar.Championships.Service.Abstraction;

namespace FormulaCar.Championships.Service;

public class RaceweekService : IRaceweekService
{
    private readonly IMapper _mapper;
    private readonly IRepositoryManager _repositoryManager;

    public RaceweekService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GrandPrixDto>> GetAll()
    {
        var grandPrixs = await _repositoryManager.RaceweekRepository.FindAll();
        var circutes = await _repositoryManager.CircuiteRepository.FindAll();
        var seasons = await _repositoryManager.SeasonRepository.FindAll();
        var grandPrixDtos = new List<GrandPrixDto>();

        foreach (var raceweek in grandPrixs)
        {
            var newRaceweek = _mapper.Map<GrandPrixDto>(raceweek);
            newRaceweek.Season = seasons.FirstOrDefault(x => x.Id == raceweek.SeasonId)?.Year.ToString();
            newRaceweek.GrandPrixName = circutes.FirstOrDefault(x => x.Id == raceweek.CircuiteId)?.Name;
            grandPrixDtos.Add(newRaceweek);
        }

        return grandPrixDtos;
    }

    public async Task<GrandPrixDto> Create(GrandPrixForCreation grandPrixForCreation)
    {
        var circutes = await _repositoryManager.CircuiteRepository.FindAll();
        var seasons = await _repositoryManager.SeasonRepository.FindAll();
        var newRaceweek = _mapper.Map<Raceweek>(grandPrixForCreation);
        var selectedSeason = seasons.FirstOrDefault(x => x.Year.ToString() == grandPrixForCreation.Season);
        var circuit =
            circutes.FirstOrDefault(x => x.Name.Contains(grandPrixForCreation.GrandPrixName.ToLower().Trim(' ')));

        if (circuit == null || selectedSeason == null) return null;

        newRaceweek.CircuiteId = circuit.Id;
        newRaceweek.SeasonId = selectedSeason.Id;
        newRaceweek.Results = new List<Result>();
        _repositoryManager.RaceweekRepository.Insert(newRaceweek);
        await _repositoryManager.UnitOfWork.SaveChangesAsync();

        var createdRaceweek = _mapper.Map<GrandPrixDto>(newRaceweek);
        createdRaceweek.Season = seasons.FirstOrDefault(x => x.Id == newRaceweek.SeasonId)?.Year.ToString();
        createdRaceweek.GrandPrixName = circutes.FirstOrDefault(x => x.Id == newRaceweek.CircuiteId)?.Name;

        return createdRaceweek;
    }

    public async Task<bool> Exist(string grandPrix, string season)
    {
        var circutes = await _repositoryManager.CircuiteRepository.FindAll();
        var seasons = await _repositoryManager.SeasonRepository.FindAll();
        var selectedSeason = seasons.FirstOrDefault(x => x.Year.ToString() == season);
        var selectedCircuit =
            circutes.FirstOrDefault(x => x.Name.Contains(grandPrix.ToLower().Trim(' ')));


        if (selectedCircuit == null || selectedSeason == null) throw new ItemNotFoundException(-1);


        var selectedRaceweek = await _repositoryManager.RaceweekRepository.FindByCondition(x =>
            x.SeasonId == selectedSeason.Id && x.Circuite.Id == selectedCircuit.Id);

        return selectedRaceweek.Any();
    }
}