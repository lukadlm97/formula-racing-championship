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
            var bookings = await _repositoryManager.BookingRepository.FindAll();
            var raceweeks = await _repositoryManager.RaceweekRepository.FindAll();
            var constructors = await _repositoryManager.ConstructorRepository.FindAll();
            var drivers = await _repositoryManager.DriverRepository.FindByCondition(x=>x.IsActive);
            var circuite = await _repositoryManager.CircuiteRepository.FindAll();
            var position =await  _repositoryManager.PositionRepository.FindAll();

            List<RaceResultItemDto> raceResultItemDtos = new List<RaceResultItemDto>();

            foreach (var raceClassification in results)
            {
                var raceResult = _mapper.Map<RaceResultItemDto>(raceClassification);
                var selectedBooking = bookings.FirstOrDefault(x => x.Id == raceClassification.BookingId);
                if (selectedBooking == null)
                {
                    throw new ItemNotFoundException(raceClassification.Id);
                }

                var selectedDriver = drivers.FirstOrDefault(x => x.Id == selectedBooking.DriverId);
                var selectedConstructor = constructors.FirstOrDefault(x => x.Id == selectedBooking.ConstructorId);

                var selectedRaceweek = raceweeks.FirstOrDefault(x => x.Id == raceClassification.RaceweekId); 
                if (selectedRaceweek == null)
                {
                    throw new ItemNotFoundException(raceClassification.Id);
                }

                var selectedCircute = circuite.FirstOrDefault(x => x.Id == selectedRaceweek.CircuiteId);


                if (selectedCircute == null || selectedDriver == null || selectedConstructor == null)
                {
                    throw new ItemNotFoundException(raceClassification.Id);
                }

                raceResult.Driver = selectedDriver.FirstName + " " + selectedDriver.LastName;
                raceResult.Constructor = selectedConstructor.Name;
                raceResult.Circuite=selectedCircute.Name;
                raceResult.Position = position.FirstOrDefault(x => x.Id == raceClassification.PositionId).Rank;
                raceResultItemDtos.Add(raceResult);
            }

            return raceResultItemDtos;
        }

        public async Task<RaceResultItemDto> Create(RaceResultItemForCreationDto raceResultItemForCreationDto)
        {
            var selectedPosition =
                (await _repositoryManager.PositionRepository.FindByCondition(x => x.Rank == raceResultItemForCreationDto.Postion)).FirstOrDefault();
            var driverSplited = raceResultItemForCreationDto.Driver.Split(' ');
            var firstName = driverSplited[0];
            var lastName = driverSplited[1];
            var selectedDriver = (await _repositoryManager.DriverRepository.FindByCondition(x =>
                x.FirstName.ToLower() == firstName.ToLower() && x.LastName.ToLower() == lastName.ToLower())).FirstOrDefault();
            if (selectedDriver == null)
            {
                if (firstName.ToLower() == "Alexander".ToLower())
                {
                    firstName = "Alex";
                }
            }
            var selectedSeason =
                (await _repositoryManager.SeasonRepository.FindByCondition(x =>
                    x.Year.ToString().ToLower() == raceResultItemForCreationDto.Season.ToString())).FirstOrDefault();
            selectedDriver = (await _repositoryManager.DriverRepository.FindByCondition(x =>
                x.FirstName.ToLower() == firstName.ToLower() && x.LastName.ToLower() == lastName.ToLower())).FirstOrDefault();
            var selectedBooking = (await _repositoryManager.BookingRepository.FindByCondition(x =>
                x.DriverId == selectedDriver.Id && x.IsActive && x.SeasonId==selectedSeason.Id)).FirstOrDefault();
            var selectedCircuite = (
                await _repositoryManager.CircuiteRepository.FindByCondition(x =>
                    x.Name.ToLower() == raceResultItemForCreationDto.Circuite.ToLower())).FirstOrDefault();
            var selectedRaceweek = (await _repositoryManager.RaceweekRepository.FindByCondition(x =>
                x.CircuiteId == selectedCircuite.Id && x.SeasonId ==
                selectedSeason.Id)).OrderBy(x => x.OrderNumber).LastOrDefault();

            if (selectedRaceweek == null || selectedPosition == null || selectedBooking == null)
            {
                return null;
            }

            RaceClassification newRaceClassification = new RaceClassification()
            {
                Laps = raceResultItemForCreationDto.Laps,
                Time = raceResultItemForCreationDto.Time,
                BookingId = selectedBooking.Id,
                PositionId = selectedPosition.Id,
                RaceweekId = selectedRaceweek.Id
            };

            _repositoryManager.RaceClassificationRepository.Insert(newRaceClassification);
            await _repositoryManager.UnitOfWork.SaveChangesAsync();

            return _mapper.Map<RaceResultItemDto>(newRaceClassification);
        }

        public Task<IEnumerable<RaceResultItemDto>> GetByRace(int raceweekId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Exist(string driver, string circuit, string season, string position)
        {
            var selectedPosition =
                (await _repositoryManager.PositionRepository.FindByCondition(x => x.Rank == position)).FirstOrDefault();
            var driverSplited = driver.Split(' ');
            var firstName = driverSplited[0];
            var lastName = driverSplited[1];
            var selectedDriver = (await _repositoryManager.DriverRepository.FindByCondition(x =>
                x.FirstName.ToLower() == firstName.ToLower() && x.LastName.ToLower() == lastName.ToLower())).FirstOrDefault();
            if (selectedDriver == null)
            {
                if (firstName.ToLower() == "Alexander".ToLower())
                {
                    firstName = "Alex";
                }
            }
            var selectedSeason =
                (await _repositoryManager.SeasonRepository.FindByCondition(x =>
                    x.Year.ToString().ToLower() == season)).FirstOrDefault();
            selectedDriver = (await _repositoryManager.DriverRepository.FindByCondition(x =>
                x.FirstName.ToLower() == firstName.ToLower() && x.LastName.ToLower() == lastName.ToLower())).FirstOrDefault();
            var selectedBooking = (await _repositoryManager.BookingRepository.FindByCondition(x =>
                x.DriverId == selectedDriver.Id && x.IsActive&&x.SeasonId==selectedSeason.Id)).FirstOrDefault();
            var selectedCircuite =(
                await _repositoryManager.CircuiteRepository.FindByCondition(x =>
                    x.Name.ToLower() == circuit)).FirstOrDefault();
           
            var selectedRaceweek = (await _repositoryManager.RaceweekRepository.FindByCondition(x =>
                x.CircuiteId == selectedCircuite.Id && x.SeasonId ==
                selectedSeason.Id)).OrderBy(x=>x.OrderNumber).LastOrDefault();


            if (selectedPosition == null || selectedBooking == null || selectedRaceweek == null)
            {
                return false;
            }

            return (await _repositoryManager.RaceClassificationRepository.FindByCondition(x 
                    =>
                x.BookingId == selectedBooking.Id && x.RaceweekId == selectedRaceweek.Id))
                .Any();

        }
    }
}
