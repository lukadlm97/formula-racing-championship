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
    public class RegulationRuleConfiguration:IEntityTypeConfiguration<RegulationRule>
    {
        
        public void Configure(EntityTypeBuilder<RegulationRule> builder)
        {
            builder.ToTable("RegulationRules");
            builder.HasKey(regulationRule => regulationRule.Id);
            builder.Property(regulationRule => regulationRule.Id).ValueGeneratedOnAdd();
        }
    }
}
