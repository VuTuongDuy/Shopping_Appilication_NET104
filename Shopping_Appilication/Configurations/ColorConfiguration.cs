using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping_Appilication.Models;

namespace Shopping_Appilication.Configurations
{
    public class ColorConfiguration : IEntityTypeConfiguration<Color>
    {
        public void Configure(EntityTypeBuilder<Color> builder)
        {
            builder.ToTable("Color");
            builder.HasKey(c => c.IdColor);
            builder.Property(c => c.Name).HasColumnType("nvarchar(100)").
                IsRequired();
            builder.Property(c => c.Status).HasColumnType("int").
                IsRequired();
        }
    }
}
