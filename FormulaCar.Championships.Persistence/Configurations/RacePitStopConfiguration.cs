﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormulaCar.Championships.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FormulaCar.Championships.Persistence.Configurations
{
    public class RacePitStopConfiguration:IEntityTypeConfiguration<RacePitStop>
    {
        public void Configure(EntityTypeBuilder<RacePitStop> builder)
        {
            builder.ToTable("RacePitStops");
        }
    }
}