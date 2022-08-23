using AutoMapper;
using FormulaCar.Championships.Contracts;
using FormulaCar.Championships.Domain.Entities;

namespace FormulaCar.Championships.Service.Mappers;

public class RaceSectorProfile : Profile
{
    public RaceSectorProfile()
    {
        CreateMap<RaceSectorTime, RaceBestSectorTimesDto>();
    }
}