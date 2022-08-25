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
    public class ScorePointRegulationConfiguration:IEntityTypeConfiguration<ScorePointRegulation>
    {
        public void Configure(EntityTypeBuilder<ScorePointRegulation> builder)
        {
            builder.ToTable("ScorePointRegulations");
            builder.HasKey(scorePointRegulation => scorePointRegulation.Id);
            builder.Property(scorePointRegulation => scorePointRegulation.Id).ValueGeneratedOnAdd();
            builder.HasOne(scorePointRegulation => scorePointRegulation.Position);
            builder.HasOne(scorePointRegulation => scorePointRegulation.RegulationRule);

        }
    }
}
