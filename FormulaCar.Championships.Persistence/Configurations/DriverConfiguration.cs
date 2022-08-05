using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormulaCar.Championships.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FormulaCar.Championships.Persistence.Configurations
{
    public class DriverConfiguration : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> builder)
        {
            builder.ToTable("Drivers");
            builder.HasKey(driver => driver.Id);
            builder.Property(driver => driver.Id).ValueGeneratedOnAdd();
            builder.Property(driver => driver.FirstName).HasMaxLength(250);
            builder.Property(driver => driver.LastName).HasMaxLength(250);
            builder.HasOne(country => country.MediaTag);
            /*builder.HasMany(driver => driver.Bookings)
                .WithOne(booking => booking.Driver)
                .HasForeignKey(booking => booking.DriverId);*/
        }
    }
}
