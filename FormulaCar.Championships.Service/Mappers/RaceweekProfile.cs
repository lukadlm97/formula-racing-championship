using AutoMapper;
using FormulaCar.Championships.Contracts;
using FormulaCar.Championships.Domain.Entities;

namespace FormulaCar.Championships.Service.Mappers;

public class RaceweekProfile : Profile
{
    public RaceweekProfile()
    {
        CreateMap<Raceweek, GrandPrixDto>()
            .ForMember(dest => dest.No,
                opt => opt.MapFrom(src => src.OrderNumber))
            .ForMember(dest => dest.SprintRace,
                opt => opt.MapFrom(src => src.IsContainsSprintQualification));
        CreateMap<GrandPrixForCreation, Raceweek>()
            .ForMember(dest => dest.OrderNumber,
                opt => opt.MapFrom(src => src.No))
            .ForMember(dest => dest.IsContainsSprintQualification,
                opt => opt.MapFrom(src => src.SprintRace));
    }
}