using AutoMapper;
using FormulaCar.Championships.Contracts;
using FormulaCar.Championships.Domain.Entities;

namespace FormulaCar.Championships.Service.Mappers;

public class QualificationMaximumSpeedProfile : Profile
{
    public QualificationMaximumSpeedProfile()
    {
        CreateMap<QualificationMaximumSpeed, QualificationMaximumSpeedDto>();
    }
}