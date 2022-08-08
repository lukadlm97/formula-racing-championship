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
    public class SeasonConfiguration : IEntityTypeConfiguration<Season>
    {
        public void Configure(EntityTypeBuilder<Season> builder)
        {
            builder.ToTable("Seasons");
            builder.HasKey(season => season.Id);
            builder.Property(season => season.Id).ValueGeneratedOnAdd();
            builder.HasMany(season => season.Raceweeks).WithOne().HasForeignKey(raceweek => raceweek.SeasonId);
            builder.HasMany(season => season.Bookings).WithOne().HasForeignKey(booking => booking.SeasonId);
        }
    }
}
