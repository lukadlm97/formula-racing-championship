using FormulaCar.Championships.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FormulaCar.Championships.Persistence.Configurations;

public class RaceClassificationConfiguration : IEntityTypeConfiguration<RaceClassification>
{
    public void Configure(EntityTypeBuilder<RaceClassification> builder)
    {
        builder.ToTable("RaceClassifications");
    }
}