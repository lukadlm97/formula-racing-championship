using FormulaCar.Championships.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FormulaCar.Championships.Persistence.Configurations;

public class RaceSpeedTrapConfiguration : IEntityTypeConfiguration<RaceSpeedTrap>
{
    public void Configure(EntityTypeBuilder<RaceSpeedTrap> builder)
    {
        builder.ToTable("RaceSpeedTraps");
    }
}