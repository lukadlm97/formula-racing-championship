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
    public class RaceClassificationConfiguration:IEntityTypeConfiguration<RaceClassification>
    {
        public void Configure(EntityTypeBuilder<RaceClassification> builder)
        {
            builder.ToTable("RaceClassifications");
        }
    }
}
