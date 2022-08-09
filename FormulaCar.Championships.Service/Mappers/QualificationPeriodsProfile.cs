using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FormulaCar.Championships.Contracts;
using FormulaCar.Championships.Domain.Entities;

namespace FormulaCar.Championships.Service.Mappers
{
    public class QualificationPeriodsProfile:Profile
    {
        public QualificationPeriodsProfile()
        {
            CreateMap<QualificationPeriod,QualificationPeriodsDto>()
                .ForMember(dest => dest.QualificationPeriodsId, 
                    opt => opt.MapFrom(x => x.Id));
        }
    }
}
