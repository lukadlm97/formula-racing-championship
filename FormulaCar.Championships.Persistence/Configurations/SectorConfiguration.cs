using FormulaCar.Championships.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FormulaCar.Championships.Persistence.Configurations;

public class SectorConfiguration : IEntityTypeConfiguration<Sector>
{
    public void Configure(EntityTypeBuilder<Sector> builder)
    {
        builder.ToTable("Sectors");
        builder.HasKey(sector => sector.Id);
        builder.Property(sector => sector.Id).ValueGeneratedOnAdd();
        builder.HasMany(x => x.RaceSectorTimes).WithOne().HasForeignKey(x => x.SectorId);
        builder.HasMany(x => x.RaceMaximumSpeeds).WithOne().HasForeignKey(x => x.SectorId);
        builder.HasMany(x => x.QualificationBestSectorTimes).WithOne().HasForeignKey(x => x.SectorId);
        builder.HasMany(x => x.QualificationMaximumSpeeds).WithOne().HasForeignKey(x => x.SectorId);
    }
}