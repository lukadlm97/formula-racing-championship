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
    public class QualificationMaximumSpeedsConfiguration : IEntityTypeConfiguration<QualificationMaximumSpeed>
    {
        public void Configure(EntityTypeBuilder<QualificationMaximumSpeed> builder)
        {

            builder.ToTable("QualificationMaximumSpeeds");
        }
    }
}
