using AutoMapper;
using FormulaCar.Championships.Contracts;
using FormulaCar.Championships.Domain.Entities;

namespace FormulaCar.Championships.Service.Mappers;

public class QualificationBestSectorProfile : Profile
{
    public QualificationBestSectorProfile()
    {
        CreateMap<QualificationBestSectorTimes, QualificationBestSectorTimeDto>();
    }
}