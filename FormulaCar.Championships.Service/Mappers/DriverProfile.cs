using AutoMapper;
using FormulaCar.Championships.Contracts;
using FormulaCar.Championships.Domain.Entities;

namespace FormulaCar.Championships.Service.Mappers;

public class DriverProfile : Profile
{
    public DriverProfile()
    {
        CreateMap<Driver, DriverDto>();
        CreateMap<DriverForCreationDto, Driver>();
    }
}