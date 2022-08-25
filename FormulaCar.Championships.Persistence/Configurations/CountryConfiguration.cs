﻿using FormulaCar.Championships.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FormulaCar.Championships.Persistence.Configurations;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.ToTable("Countries");
        builder.HasKey(country => country.Id);
        builder.Property(country => country.Id).ValueGeneratedOnAdd();
        builder.Property(country => country.Name).HasMaxLength(250);
        builder.HasOne(country => country.MediaTag);
        builder.HasMany(country => country.Drivers)
            .WithOne()
            .HasForeignKey(driver => driver.CountryId);
        builder.HasMany(country => country.Constructors)
            .WithOne()
            .HasForeignKey(constructor => constructor.CountryId);
        builder.HasMany(country => country.Circuites)
            .WithOne()
            .HasForeignKey(circuite => circuite.CountryId);  
        builder.HasMany(country => country.Engines)
            .WithOne()
            .HasForeignKey(engine => engine.CountryId);
    }
}