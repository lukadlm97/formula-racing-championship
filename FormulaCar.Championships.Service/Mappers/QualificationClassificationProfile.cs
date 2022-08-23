using AutoMapper;
using FormulaCar.Championships.Contracts;
using FormulaCar.Championships.Domain.Entities;

namespace FormulaCar.Championships.Service.Mappers;

public class QualificationClassificationProfile : Profile
{
    public QualificationClassificationProfile()
    {
        CreateMap<QualificationClassification, QualificationClassificationDto>();
    }
}