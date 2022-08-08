using FormulaCar.Championships.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FormulaCar.Championships.Persistence.Configurations;

internal class CircuiteConfiguration : IEntityTypeConfiguration<Circuite>
{
    public void Configure(EntityTypeBuilder<Circuite> builder)
    {
        builder.ToTable("Circuites");
        builder.HasKey(circuite => circuite.Id);
        builder.Property(circuite => circuite.Id).ValueGeneratedOnAdd();
        builder.Property(circuite => circuite.Name).HasMaxLength(250);
        builder.HasOne(circuite => circuite.MediaTag);
    }
}