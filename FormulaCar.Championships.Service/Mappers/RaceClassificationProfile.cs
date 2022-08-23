using AutoMapper;
using FormulaCar.Championships.Contracts;
using FormulaCar.Championships.Domain.Entities;

namespace FormulaCar.Championships.Service.Mappers;

public class RaceClassificationProfile : Profile
{
    public RaceClassificationProfile()
    {
        CreateMap<RaceClassification, RaceResultItemDto>()
            .ForMember(dest => dest.RaceClassifciationId, opt => opt.MapFrom(src => src.Id))
            ;
        CreateMap<RaceResultItemForCreationDto, RaceClassification>();
    }
}