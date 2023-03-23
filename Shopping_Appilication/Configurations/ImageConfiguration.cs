using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping_Appilication.Models;

namespace Shopping_Appilication.Configurations
{
    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.ToTable("Image");
            builder.HasKey(c => c.IdImage);
            builder.Property(c => c.Name).HasColumnType("nvarchar(200)").
                IsRequired();
            builder.Property(c => c.Image1).HasColumnType("nvarchar(1000)").
                IsRequired();
            builder.Property(c => c.Image2).HasColumnType("nvarchar(1000)").
                IsRequired();
            builder.Property(c => c.Image3).HasColumnType("nvarchar(1000)").
                IsRequired();
            builder.Property(c => c.Image4).HasColumnType("nvarchar(1000)").
                IsRequired();
            builder.Property(c => c.Status).HasColumnType("int").
                IsRequired();
        }
    }
}
