using AutoMapper;
using FormulaCar.Championships.Contracts;
using FormulaCar.Championships.Domain.Entities;

namespace FormulaCar.Championships.Service.Mappers;

public class RacePitStopProfile : Profile
{
    public RacePitStopProfile()
    {
        CreateMap<RacePitStop, RacePitStopDto>();
    }
}