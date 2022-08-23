using AutoMapper;
using FormulaCar.Championships.Contracts;
using FormulaCar.Championships.Domain.Entities;

namespace FormulaCar.Championships.Service.Mappers;

public class QualificationSpeedTrapProfile : Profile
{
    public QualificationSpeedTrapProfile()
    {
        CreateMap<QualificationSpeedTrap, QualificationSpeedTrapDto>();
    }
}