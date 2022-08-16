using AutoMapper;
using FormulaCar.Championships.Contracts;
using FormulaCar.Championships.Domain.Entities;

namespace FormulaCar.Championships.Service.Mappers;

public class CircuitProfile : Profile
{
    public CircuitProfile()
    {
        CreateMap<Circuite, CircuitDto>()
            .ForMember(dest => dest.CircuitId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City));

        CreateMap<CircuitForCreationDto, Circuite>()
            .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.CountryId));
    }
}