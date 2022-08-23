using AutoMapper;
using FormulaCar.Championships.Contracts;
using FormulaCar.Championships.Domain.Entities;

namespace FormulaCar.Championships.Service.Mappers;

public class RaceFastestLapProfile : Profile
{
    public RaceFastestLapProfile()
    {
        CreateMap<RaceFastesLap, RaceFastestLapDto>();
    }
}