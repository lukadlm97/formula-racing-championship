using FormulaCar.Championships.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FormulaCar.Championships.Persistence.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable("Bookings");
        builder.HasKey(booking => booking.Id);
        builder.Property(booking => booking.Id).ValueGeneratedOnAdd();
        // builder.HasOne(booking => booking.Constructor).WithMany().HasForeignKey(booking=>booking.ConstructorId);
        // builder.HasOne(booking => booking.Driver).WithMany().HasForeignKey(booking => booking.DriverId);
        builder.HasMany(booking => booking.Results).WithOne().HasForeignKey(result => result.BookingId);
    }
}