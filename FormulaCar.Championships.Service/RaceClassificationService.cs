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
    public class RaceClassificationService:IRaceClassificationService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;

        public RaceClassificationService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }
        public async Task<IEnumerable<RaceResultItemDto>> GetAll()
        {
            var results = await _repositoryManager.RaceClassificationRepository.FindAll();

            List<RaceResultItemDto> raceResultItemDtos = new List<RaceResultItemDto>();

            foreach (var raceClassification in results)
            {
                var raceResult = _mapper.Map<RaceResultItemDto>(raceClassification);
                raceResultItemDtos.Add(raceResult);
            }

            return raceResultItemDtos;
        }

        public async Task<RaceResultItemDto> Create(RaceResultItemForCreationDto raceResultItemForCreationDto)
        {
            var selectedPosition =
                await _repositoryManager.PositionRepository.FindByCondition(x =>
                    x.Rank == raceResultItemForCreationDto.Postion);
            var firstName = raceResultItemForCreationDto.Driver.Split(' ')[0];
            var lastName = raceResultItemForCreationDto.Driver.Split(' ')[1];
            var selectedDriver = await _repositoryManager.DriverRepository.FindByCondition(x =>
                x.FirstName.ToLower() == firstName.ToLower() && x.LastName.ToLower() == lastName.ToLower());
            var selectedBooking = await _repositoryManager.BookingRepository.FindByCondition(x =>
                x.DriverId == selectedDriver.FirstOrDefault().Id && x.IsActive);
            var selectedCircuite =
                await _repositoryManager.CircuiteRepository.FindByCondition(x =>
                    x.Name.ToLower() == raceResultItemForCreationDto.Circuite);
            var selectedSeason =
                await _repositoryManager.SeasonRepository.FindByCondition(x =>
                    x.Year.ToString().ToLower() == raceResultItemForCreationDto.Season.ToString());
            var selectedRaceweek = await _repositoryManager.RaceweekRepository.FindByCondition(x =>
                x.CircuiteId == selectedCircuite.FirstOrDefault().Id && x.SeasonId ==
                    selectedSeason.FirstOrDefault().Id);

            RaceClassification newRaceClassification = new RaceClassification()
            {
                Laps = raceResultItemForCreationDto.Laps,
                Time = raceResultItemForCreationDto.Time,
                BookingId = selectedBooking.FirstOrDefault().Id,
                Position = selectedPosition.FirstOrDefault(),
                RaceweekId = selectedRaceweek.FirstOrDefault().Id
            };

            _repositoryManager.RaceClassificationRepository.Insert(newRaceClassification);
            await _repositoryManager.UnitOfWork.SaveChangesAsync();

            return _mapper.Map<RaceResultItemDto>(newRaceClassification);
        }

        public Task<IEnumerable<RaceResultItemDto>> GetByRace(int raceweekId)
        {
            throw new NotImplementedException();
        }
    }
}
