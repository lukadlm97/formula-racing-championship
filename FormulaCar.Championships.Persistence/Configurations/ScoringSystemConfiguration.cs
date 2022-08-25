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
    public class ScoringSystemConfiguration:IEntityTypeConfiguration<ScoringSystem>
    {
        public void Configure(EntityTypeBuilder<ScoringSystem> builder)
        {
            builder.ToTable("ScoringSystems");
            builder.HasKey(scoringSystem => scoringSystem.Id);
            builder.Property(scoringSystem => scoringSystem.Id).ValueGeneratedOnAdd();
            builder.HasOne(scoringSystem => scoringSystem.Raceweek);
            builder.HasOne(scoringSystem => scoringSystem.RegulationRule);
        }
    }
}
