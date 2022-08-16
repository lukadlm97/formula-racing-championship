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
    public class ConstructorService:IConstructorService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public ConstructorService(IRepositoryManager repositoryManager,IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }
        public async Task<ConstructorDto> CreateConstructor(ConstructorForCreationDto constructorForCreationDto, CancellationToken cancellationToken = default)
        {
            var newConstructor = _mapper.Map<Constructor>(constructorForCreationDto);
            var countries = await _repositoryManager.CountryRepository.FindAll(cancellationToken);
            var country = countries.FirstOrDefault(x => x.Name.ToLower().Contains(constructorForCreationDto.CountryCode.ToLower()));
            if (country == null)
            {
                return null;
            }

            newConstructor.CountryId=country.Id;
            _repositoryManager.ConstructorRepository.Insert(newConstructor);
            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ConstructorDto>(newConstructor);
        }

        public async Task<IEnumerable<ConstructorDto>> GetConstructors( CancellationToken cancellationToken = default)
        {
            List<ConstructorDto> countryDtos = new List<ConstructorDto>();
            var constructors = await _repositoryManager.ConstructorRepository.FindAll(cancellationToken);
            var countries = await _repositoryManager.CountryRepository.FindAll(cancellationToken);

            foreach (var constructor in constructors)
            {
                var newConstructor = _mapper.Map<ConstructorDto>(constructor);
                var country = countries.FirstOrDefault(x => x.Id == constructor.CountryId);
                if (country == null)
                {
                    throw new ItemNotFoundException(constructor.CountryId);
                }

                newConstructor.Country = country.Name;
                countryDtos.Add(newConstructor);
            }
            
            return countryDtos;
        }

        public async Task<bool> Exist(string name, CancellationToken cancellationToken = default)
        {
            var constructor = await 
                _repositoryManager.ConstructorRepository.FindByCondition(x =>
                    x.Name.ToLower().Contains(name.ToLower()), cancellationToken);

            return constructor != null && constructor.Any();
        }
    }
}
