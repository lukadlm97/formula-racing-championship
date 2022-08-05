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
    public class MediaTagConfiguration:IEntityTypeConfiguration<MediaTag>
    {
        public void Configure(EntityTypeBuilder<MediaTag> builder)
        {
            builder.ToTable("MediaTags");
            builder.HasKey(mediaTag => mediaTag.Id);
            builder.Property(mediaTag => mediaTag.Id).ValueGeneratedOnAdd();

        }
    }
}
