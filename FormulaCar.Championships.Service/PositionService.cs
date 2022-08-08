using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormulaCar.Championships.Contracts;
using FormulaCar.Championships.Domain.Entities;
using FormulaCar.Championships.Domain.Repositories;
using FormulaCar.Championships.Service.Abstraction;
using Mapster;

namespace FormulaCar.Championships.Service
{
    public class PositionService:IPositionService
    {
        private readonly IRepositoryManager _repositoryManager;
        public PositionService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<PositionDto> Create(PositionForCreationDto positionDto)
        {
            var position = positionDto.Adapt<Position>();

            _repositoryManager.PositionRepository.Insert(position);
            await _repositoryManager.UnitOfWork.SaveChangesAsync();

            return position.Adapt<PositionDto>();
        }

        public async Task<PositionDto> Update(PositionForUpdateDto positionDto)
        {
            var position = (await _repositoryManager.PositionRepository.FindByCondition(x => x.Id == positionDto.Id)).FirstOrDefault();
            if (position is null)
            {
                throw new NullReferenceException(positionDto.Id+" not exist!!!");
            }

            position.Rank = positionDto.Name;

            await _repositoryManager.UnitOfWork.SaveChangesAsync();
            return position.Adapt<PositionDto>();
        }

        public async Task<bool> Delete(int positionId)
        {
            var position = (await _repositoryManager.PositionRepository.FindByCondition(x => x.Id == positionId)).FirstOrDefault();
            if (position is null)
            {
                throw new NullReferenceException(positionId + " not exist!!!");
            }

            _repositoryManager.PositionRepository.Remove(position);
            await _repositoryManager.UnitOfWork.SaveChangesAsync();


            return true;
        }

        public async Task<IEnumerable<PositionDto>> GetAll()
        {
            var positions = await _repositoryManager.PositionRepository.FindAll();
            if (positions is null)
            {
                return new List<PositionDto>();
            }


            var positionsDto = new List<PositionDto>();
            foreach (var position in positions)
            {
                var newPosition = new PositionDto()
                {
                    Id = position.Id,
                    Name = position.Rank
                };
                positionsDto.Add(newPosition);
            }
            return positionsDto;
        }
    }
}
