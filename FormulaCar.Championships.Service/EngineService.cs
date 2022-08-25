using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FormulaCar.Championships.Contracts;
using FormulaCar.Championships.Domain.Entities;
using FormulaCar.Championships.Domain.Repositories;
using FormulaCar.Championships.Service.Abstraction;

namespace FormulaCar.Championships.Service
{
    public class EngineService:IEngineService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public EngineService(IRepositoryManager repositoryManager,IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }
        public Task<IEnumerable<EngineDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<EngineDto> Create(EngineForCreationDto engineForCreationDto)
        {
            var selectedEngine = (
                await _repositoryManager.EngineRepository.FindByCondition(x =>
                    x.Manufacturer.ToLower() == engineForCreationDto.Manufacturer.ToLower())).FirstOrDefault();
            var selectedCountry = await GetIdByName(engineForCreationDto.Country);

            if (selectedCountry == -1 ||selectedEngine!=null)
            {
                return null;
            }

            var newEngine = new Engine()
            {
                CountryId = selectedCountry,
                FirstRun = engineForCreationDto.FirstRun,
                IsActive = engineForCreationDto.IsActive,
                Manufacturer = engineForCreationDto.Manufacturer
            };
            _repositoryManager.EngineRepository.Insert(newEngine);
            await _repositoryManager.UnitOfWork.SaveChangesAsync();

            return _mapper.Map<EngineDto>(newEngine);

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

        public async Task<bool> Exist(string name)
        {
            var selectedEngine =(
                await _repositoryManager.EngineRepository.FindByCondition(x =>
                    x.Manufacturer.ToLower() == name.ToLower())).FirstOrDefault();

            return selectedEngine != null;
        }
    }
}
