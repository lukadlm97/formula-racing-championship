using FormulaCar.Championships.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FormulaCar.Championships.Persistence.Configurations;

public class RaceFastesLapConfiguration : IEntityTypeConfiguration<RaceFastesLap>
{
    public void Configure(EntityTypeBuilder<RaceFastesLap> builder)
    {
        builder.ToTable("RaceFastesLaps");
    }
}