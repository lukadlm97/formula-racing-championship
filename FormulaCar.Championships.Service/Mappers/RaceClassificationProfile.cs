﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FormulaCar.Championships.Contracts;
using FormulaCar.Championships.Domain.Entities;

namespace FormulaCar.Championships.Service.Mappers
{
    public class RaceClassificationProfile:Profile
    {
        public RaceClassificationProfile()
        {
            CreateMap<RaceClassification, RaceResultItemDto>()
                ;
            CreateMap<RaceResultItemForCreationDto, RaceClassification>();
        }
    }
}