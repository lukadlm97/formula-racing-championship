using FormulaCar.Championships.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FormulaCar.Championships.Persistence.Configurations;

public class RacePitStopConfiguration : IEntityTypeConfiguration<RacePitStop>
{
    public void Configure(EntityTypeBuilder<RacePitStop> builder)
    {
        builder.ToTable("RacePitStops");
    }
}