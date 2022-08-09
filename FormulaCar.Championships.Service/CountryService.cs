using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FormulaCar.Championships.Contracts;
using FormulaCar.Championships.Domain.Entities;
using FormulaCar.Championships.Domain.Exceptions;
using FormulaCar.Championships.Domain.Repositories;
using FormulaCar.Championships.Service.Abstraction;

namespace FormulaCar.Championships.Service
{
    public class CountryService:ICountryService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public CountryService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CountryDto>> GetCountries(CancellationToken cancellationToken)
        {
            var countries = await _repositoryManager.CountryRepository.GetAllWithMedia();
            if (countries == null )
            {
                throw new ItemsNotFoundException(nameof(Country));
            }

            var countriesDto = new List<CountryDto>();
            foreach (var item in countries)
            {
                countriesDto.Add(_mapper.Map<CountryDto>(item));
            }

            return countriesDto;
        }

        public async Task<CountryDto> Create(CountryForCreationDto countryForCreationDto, CancellationToken cancellationToken)
        {
            var country = _mapper.Map<Country>(countryForCreationDto);
            

            await _repositoryManager.CountryRepository.InsertCountry(country, countryForCreationDto.MediaTagId);
            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<CountryDto>(country);
        }
    }
}
