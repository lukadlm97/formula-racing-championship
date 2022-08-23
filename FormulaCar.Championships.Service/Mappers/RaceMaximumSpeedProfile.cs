using AutoMapper;
using FormulaCar.Championships.Contracts;
using FormulaCar.Championships.Domain.Entities;

namespace FormulaCar.Championships.Service.Mappers;

public class RaceMaximumSpeedProfile : Profile
{
    public RaceMaximumSpeedProfile()
    {
        CreateMap<RaceMaximumSpeed, RaceMaximumSpeedDto>();
    }
}