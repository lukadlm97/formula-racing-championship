using FormulaCar.Championships.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FormulaCar.Championships.Persistence.Configurations;

public class ConstructorConfiguration : IEntityTypeConfiguration<Constructor>
{
    public void Configure(EntityTypeBuilder<Constructor> builder)
    {
        builder.ToTable("Constructors");
        builder.HasKey(constructor => constructor.Id);
        builder.Property(constructor => constructor.Id).ValueGeneratedOnAdd();
        builder.Property(constructor => constructor.Name).HasMaxLength(250);
        builder.HasOne(constructor => constructor.MediaTag);
        builder.HasMany(constructor => constructor.Bookings).WithOne()
            .HasForeignKey(constructor => constructor.ConstructorId);
    }
}