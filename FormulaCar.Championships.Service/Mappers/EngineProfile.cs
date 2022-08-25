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
    public class EngineProfile:Profile
    {
        public EngineProfile()
        {
            CreateMap<Engine, EngineDto>();
        }
    }
}
