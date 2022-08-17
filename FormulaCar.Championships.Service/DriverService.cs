using AutoMapper;
using FormulaCar.Championships.Contracts;
using FormulaCar.Championships.Domain.Entities;
using FormulaCar.Championships.Domain.Repositories;
using FormulaCar.Championships.Service.Abstraction;

namespace FormulaCar.Championships.Service;

public class DriverService : IDriverService
{
    private readonly IMapper _mapper;
    private readonly IRepositoryManager _repositoryManager;

    public DriverService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DriverDto>> GetDrivers()
    {
        var driverDtos = new List<DriverDto>();
        var drivers = await _repositoryManager.DriverRepository.FindByCondition(x=>x.IsActive==true);
        if (!drivers.Any()) return driverDtos;

        var countries = await _repositoryManager.CountryRepository.FindAll();
        foreach (var driver in drivers)
        {
            var newDriver = _mapper.Map<DriverDto>(driver);
            newDriver.Years = DateTime.Now.Year - driver.DateOfBirth.Year;
            newDriver.Country = countries.FirstOrDefault(x => x.Id == driver.CountryId)?.Name;
            driverDtos.Add(newDriver);
        }

        return driverDtos;
    }

    public async Task<DriverDto> Create(DriverForCreationDto driver)
    {
        var countries = await _repositoryManager.CountryRepository.FindAll();

        var newDriver = _mapper.Map<Driver>(driver);
        var origin = GetOrigin(driver.Nationality, countries);
        if (origin == null) return null;
        newDriver.CountryId = GetOrigin(driver.Nationality, countries).Id;

        _repositoryManager.DriverRepository.Insert(newDriver);
        await _repositoryManager.UnitOfWork.SaveChangesAsync();

        var createdDriver = _mapper.Map<DriverDto>(newDriver);
        createdDriver.Country = countries.FirstOrDefault(x => x.Id == newDriver.CountryId)?.Name;
        return createdDriver;
    }

    public async Task<bool> Exist(string firstName, string lastName)
    {
        return _repositoryManager.DriverRepository.Exist(firstName, lastName);
    }


    private Country GetOrigin(string nationality, IEnumerable<Country> countries)
    {
        char[] charsToTrim = { '*', ' ', '\'' };
        var result = nationality.Trim(charsToTrim);
        var country = countries.FirstOrDefault();

        switch (result)
        {
            case "British":
                country = countries.FirstOrDefault(x => x.Code.ToLower().Contains("UK".ToLower()));
                break;
            case "German":
                country = countries.FirstOrDefault(x => x.Code.ToLower().Contains("DEU".ToLower()));
                break;
            case "Spanish":
                country = countries.FirstOrDefault(x => x.Code.ToLower().Contains("ESP".ToLower()));
                break;
            case "Finnish":
                country = countries.FirstOrDefault(x => x.Code.ToLower().Contains("FIN".ToLower()));
                break;
            case "Japanese":
                country = countries.FirstOrDefault(x => x.Code.ToLower().Contains("JPN".ToLower()));
                break;
            case "French":
                country = countries.FirstOrDefault(x => x.Code.ToLower().Contains("FRA".ToLower()));
                break;
            case "Polish":
                country = countries.FirstOrDefault(x => x.Code.ToLower().Contains("POL".ToLower()));
                break;
            case "Brazilian":
                country = countries.FirstOrDefault(x => x.Code.ToLower().Contains("BRA".ToLower()));
                break;
            case "Italian":
                country = countries.FirstOrDefault(x => x.Code.ToLower().Contains("ITA".ToLower()));
                break;
            case "Australian":
                country = countries.FirstOrDefault(x => x.Code.ToLower().Contains("AUS".ToLower()));
                break;
            case "Austrian":
                country = countries.FirstOrDefault(x => x.Code.ToLower().Contains("AUT".ToLower()));
                break;
            case "American":
                country = countries.FirstOrDefault(x => x.Code.ToLower().Contains("USA".ToLower()));
                break;
            case "Dutch":
                country = countries.FirstOrDefault(x => x.Code.ToLower().Contains("NLD".ToLower()));
                break;
            case "Colombian":
                country = countries.FirstOrDefault(x => x.Code.ToLower().Contains("COL".ToLower()));
                break;
            case "Portuguese":
                country = countries.FirstOrDefault(x => x.Code.ToLower().Contains("PRT".ToLower()));
                break;
            case "Canadian":
                country = countries.FirstOrDefault(x => x.Code.ToLower().Contains("CAN".ToLower()));
                break;
            case "Indian":
                country = countries.FirstOrDefault(x => x.Code.ToLower().Contains("IND".ToLower()));
                break;
            case "Hungarian":
                country = countries.FirstOrDefault(x => x.Code.ToLower().Contains("HUN".ToLower()));
                break;
            case "Irish":
                country = countries.FirstOrDefault(x => x.Code.ToLower().Contains("IRL".ToLower()));
                break;
            case "Danish":
                country = countries.FirstOrDefault(x => x.Code.ToLower().Contains("DNK".ToLower()));
                break;
            case "Argentine":
                country = countries.FirstOrDefault(x => x.Code.ToLower().Contains("ARG".ToLower()));
                break;
            case "Czech":
                country = countries.FirstOrDefault(x => x.Code.ToLower().Contains("CZE".ToLower()));
                break;
            case "Malaysian":
                country = countries.FirstOrDefault(x => x.Code.ToLower().Contains("MYS".ToLower()));
                break;
            case "Swiss":
                country = countries.FirstOrDefault(x => x.Code.ToLower().Contains("CHE".ToLower()));
                break;
            case "Belgian":
                country = countries.FirstOrDefault(x => x.Code.ToLower().Contains("BEL".ToLower()));
                break;
            case "Swedish":
                country = countries.FirstOrDefault(x => x.Code.ToLower().Contains("SWE".ToLower()));
                break;
            case "New Zealander":
                country = countries.FirstOrDefault(x => x.Code.ToLower().Contains("NZL".ToLower()));
                break;
            case "Chilean":
                country = countries.FirstOrDefault(x => x.Code.ToLower().Contains("CHL".ToLower()));
                break;
            case "Mexican":
                country = countries.FirstOrDefault(x => x.Code.ToLower().Contains("MEX".ToLower()));
                break;
            case "South African":
                country = countries.FirstOrDefault(x => x.Code.ToLower().Contains("ZAF".ToLower()));
                break;
            case "Russian":
                country = countries.FirstOrDefault(x => x.Code.ToLower().Contains("RUS".ToLower()));
                break;
            case "Monegasque":
                country = countries.FirstOrDefault(x => x.Code.ToLower().Contains("MCO".ToLower()));
                break; 
            case "Thai":
                country = countries.FirstOrDefault(x => x.Code.ToLower().Contains("THA".ToLower()));
                break;


            default:
                Console.WriteLine("Error");
                break;
        }

        ;

        return country;
    }
}