using AutoMapper;
using FormulaCar.Championships.Contracts;
using FormulaCar.Championships.Domain.Entities;

namespace FormulaCar.Championships.Service.Mappers;

public class CountryProfile : Profile
{
    public CountryProfile()
    {
        CreateMap<Country, CountryDto>()
            .ForMember(dest => dest.CountryId,
                opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.MediaTagId,
                opt => opt.MapFrom(src => src.MediaTag.Id));
        CreateMap<CountryForCreationDto, Country>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.OriginalName));
        CreateMap<CountryForCreationDto, MediaTag>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.MediaTagId));
    }
}