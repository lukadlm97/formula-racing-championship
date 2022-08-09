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
using Mapster;

namespace FormulaCar.Championships.Service
{
    public class PositionService:IPositionService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public PositionService(IRepositoryManager repositoryManager,IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<PositionDto> Create(PositionForCreationDto positionDto, CancellationToken cancellationToken = default)
        {
            var position = _mapper.Map<Position>(positionDto);

            _repositoryManager.PositionRepository.Insert(position);
            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<PositionDto>(position);
        }

        public async Task<PositionDto> Update(PositionForUpdateDto positionDto, CancellationToken cancellationToken = default)
        {
            var position = (await _repositoryManager.PositionRepository.FindByCondition(x => x.Id == positionDto.Id, cancellationToken)).FirstOrDefault();
            if (position is null)
            {
                throw new ItemNotFoundException(positionDto.Id);
            }

            position.Rank = positionDto.Name;

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
            return _mapper.Map<PositionDto>(position);
        }

        public async Task<bool> Delete(int positionId, CancellationToken cancellationToken = default)
        {
            var position = (await _repositoryManager.PositionRepository.FindByCondition(x => x.Id == positionId, cancellationToken)).FirstOrDefault();
            if (position is null)
            {
                throw new ItemNotFoundException(positionId);
            }

            _repositoryManager.PositionRepository.Remove(position);
            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);


            return true;
        }

        public async Task<IEnumerable<PositionDto>> GetAll(CancellationToken cancellationToken = default)
        {
            var positions = await _repositoryManager.PositionRepository.FindAll(cancellationToken);
            if (positions is null)
            {
                return new List<PositionDto>();
            }
            
            var positionsDto = new List<PositionDto>();
            foreach (var position in positions)
            {
                var newPosition = _mapper.Map<PositionDto>(position);
                positionsDto.Add(newPosition);
            }
            return positionsDto;
        }

        public async Task<PositionDto> GetById(int id, CancellationToken cancellationToken = default)
        {
            var position = (await _repositoryManager.PositionRepository.FindByCondition(x => x.Id == id, cancellationToken)).FirstOrDefault();

            if (position is null)
            {
                throw new ItemNotFoundException(id);
            }
            

            return _mapper.Map<PositionDto>(position);
        }
    }
}
