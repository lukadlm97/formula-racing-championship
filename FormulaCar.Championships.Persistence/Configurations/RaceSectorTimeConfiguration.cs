﻿using FormulaCar.Championships.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FormulaCar.Championships.Persistence.Configurations;

public class RaceSectorTimeConfiguration : IEntityTypeConfiguration<RaceSectorTime>
{
    public void Configure(EntityTypeBuilder<RaceSectorTime> builder)
    {
        builder.ToTable("RaceSectorTimes");
        builder.HasOne(raceSectorTime => raceSectorTime.Sector);
    }
}