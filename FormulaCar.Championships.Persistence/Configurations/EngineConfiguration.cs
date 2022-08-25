using FormulaCar.Championships.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaCar.Championships.Persistence.Configurations
{
    public class EngineConfiguration : IEntityTypeConfiguration<Engine>
    {
        public void Configure(EntityTypeBuilder<Engine> builder)
        {
            builder.ToTable("Engines");
            builder.HasKey(engine => engine.Id);
            builder.Property(engine => engine.Id).ValueGeneratedOnAdd();
            builder.Property(engine => engine.Manufacturer).HasMaxLength(250);
            /*builder.HasMany(driver => driver.Bookings)
                .WithOne(booking => booking.Driver)
                .HasForeignKey(booking => booking.DriverId);*/
        }
    }
}
