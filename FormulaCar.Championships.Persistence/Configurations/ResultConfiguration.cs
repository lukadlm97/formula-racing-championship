using FormulaCar.Championships.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FormulaCar.Championships.Persistence.Configurations;

public class ResultConfiguration : IEntityTypeConfiguration<Result>
{
    public void Configure(EntityTypeBuilder<Result> builder)
    {
        builder.ToTable("Result");
        builder.HasKey(result => result.Id);
        builder.Property(result => result.Id).ValueGeneratedOnAdd();
        builder.HasOne(result => result.Position);
        //builder.HasOne(result => result.RaceClassification);
    }
}