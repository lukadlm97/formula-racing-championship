﻿using AutoMapper;
using FormulaCar.Championships.Contracts;
using FormulaCar.Championships.Domain.Entities;

namespace FormulaCar.Championships.Service.Mappers;

public class RaceSpeedTrapProfile : Profile
{
    public RaceSpeedTrapProfile()
    {
        CreateMap<RaceSpeedTrap, RaceSpeedTrapDto>();
    }
}