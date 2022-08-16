using AutoMapper;
using FormulaCar.Championships.Contracts;
using FormulaCar.Championships.Domain.Entities;

namespace FormulaCar.Championships.Service.Mappers;

public class PositionProfile : Profile
{
    public PositionProfile()
    {
        CreateMap<Position, PositionDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Rank));
        CreateMap<PositionForCreationDto, Position>()
            .ForMember(dest => dest.Rank, opt => opt.MapFrom(src => src.PositionName));
    }
}