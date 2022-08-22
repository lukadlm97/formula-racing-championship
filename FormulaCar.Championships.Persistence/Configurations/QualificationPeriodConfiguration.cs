using FormulaCar.Championships.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FormulaCar.Championships.Persistence.Configurations;

public class QualificationPeriodConfiguration : IEntityTypeConfiguration<QualificationPeriod>
{
    public void Configure(EntityTypeBuilder<QualificationPeriod> builder)
    {
        builder.ToTable("QualificationPeriods");
        builder.HasKey(qualificationPeriod => qualificationPeriod.Id);
        builder.Property(qualificationPeriod => qualificationPeriod.Id).ValueGeneratedOnAdd();
        builder.HasMany(x => x.QualificationClassifications).WithOne().HasForeignKey(x => x.QualificationPeriodId);
    }
}