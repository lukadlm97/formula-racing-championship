using AutoMapper;
using FormulaCar.Championships.Contracts;
using FormulaCar.Championships.Domain.Entities;
using FormulaCar.Championships.Domain.Exceptions;
using FormulaCar.Championships.Domain.Repositories;
using FormulaCar.Championships.Service.Abstraction;

namespace FormulaCar.Championships.Service;

public class CountryService : ICountryService
{
    private readonly IMapper _mapper;
    private readonly IRepositoryManager _repositoryManager;

    public CountryService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CountryDto>> GetCountries(CancellationToken cancellationToken)
    {
        var countries = await _repositoryManager.CountryRepository.GetAllWithMedia();
        if (countries == null) throw new ItemsNotFoundException(nameof(Country));

        var countriesDto = new List<CountryDto>();
        foreach (var item in countries) countriesDto.Add(_mapper.Map<CountryDto>(item));

        return countriesDto;
    }

    public async Task<CountryDto> Create(CountryForCreationDto countryForCreationDto,
        CancellationToken cancellationToken)
    {
        var country = _mapper.Map<Country>(countryForCreationDto);


        await _repositoryManager.CountryRepository.InsertCountry(country, countryForCreationDto.MediaTagId);
        await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<CountryDto>(country);
    }

    public async Task<int> GetIdByCode(string code)
    {
        var countries = await _repositoryManager.CountryRepository.FindByCondition(x => x.Code == code);

        if (countries == null || countries.Count() != 1) return -1;

        var country = countries.FirstOrDefault(x => x.Code == code);

        return country.Id;
    }

    public async Task<int> GetIdByName(string name)
    {
        var countries =
            await _repositoryManager.CountryRepository.FindByCondition(x =>
                x.Name.ToLower().Contains(name.Trim(' ').ToLower()));

        if (countries == null || countries.Count() != 1)
        {
            if (name.ToLower() == "United States".ToLower()) return countries.FirstOrDefault().Id;
            return -1;
        }

        var country = countries.FirstOrDefault();

        return country.Id;
    }

    public async Task<string> GetCodeById(int id)
    {
        var countries = await _repositoryManager.CountryRepository.FindByCondition(x => x.Id == id);

        if (countries == null || countries.Count() != 1) throw new ItemNotFoundException(-1);

        var country = countries.FirstOrDefault(x => x.Id == id);

        return country.Code;
    }
}