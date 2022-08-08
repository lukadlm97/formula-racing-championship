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
    public class RaceSpeedTrapConfiguration : IEntityTypeConfiguration<RaceSpeedTrap>
    {
        public void Configure(EntityTypeBuilder<RaceSpeedTrap> builder)
        {
            builder.ToTable("RaceSpeedTraps");
        }
    }
}
