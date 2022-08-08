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
    public class RaceweekConfiguration : IEntityTypeConfiguration<Raceweek>
    {
        public void Configure(EntityTypeBuilder<Raceweek> builder)
        {
            builder.ToTable("Raceweeks");
            builder.HasKey(raceweek => raceweek.Id);
            builder.Property(raceweek => raceweek.Id).ValueGeneratedOnAdd();
            builder.HasOne(raceweek => raceweek.Circuite);
        }
    }
}
