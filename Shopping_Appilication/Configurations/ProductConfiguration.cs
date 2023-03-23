using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping_Appilication.Models;

namespace Shopping_Appilication.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).HasColumnType("nvarchar(300)").
                IsRequired();
            builder.Property(c => c.Price).HasColumnType("int").
                IsRequired();
            builder.Property(c => c.AvailableQuantity).HasColumnType("int").
                IsRequired();
            builder.Property(c => c.Status).HasColumnType("nvarchar(500)").
                IsRequired();
            builder.Property(c => c.Supplier).HasColumnType("nvarchar(1000)");
            builder.Property(c => c.Description).HasColumnType("nvarchar(1000)");
            builder.Property(c => c.ImageUrl).HasColumnType("nvarchar(1000)");
            builder.HasOne(c => c.Color).WithMany(v => v.Products).
                HasForeignKey(x => x.IdColor);
            builder.HasOne(c => c.Size).WithMany(v => v.Products).
                HasForeignKey(x => x.IdSize);
            builder.HasOne(c => c.Sole).WithMany(v => v.Products).
                HasForeignKey(x => x.IdSole);
            builder.HasOne(c => c.Image).WithMany(x => x.Products).
                HasForeignKey(v => v.IdImage);
        }
    }
}
