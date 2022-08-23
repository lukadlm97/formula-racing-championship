using AutoMapper;
using FormulaCar.Championships.Contracts;
using FormulaCar.Championships.Domain.Entities;

namespace FormulaCar.Championships.Service.Mappers;

public class ConstructorProfile : Profile
{
    public ConstructorProfile()
    {
        CreateMap<ConstructorForCreationDto, Constructor>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.FirstApperance,
                opt => opt.MapFrom(src => new DateTime(int.Parse(src.FirstApperance), 1, 1)));
        CreateMap<Constructor, ConstructorDto>()
            .ForMember(dest => dest.FirstApperance, opt => opt.MapFrom(src => src.FirstApperance))
            ;
    }
}