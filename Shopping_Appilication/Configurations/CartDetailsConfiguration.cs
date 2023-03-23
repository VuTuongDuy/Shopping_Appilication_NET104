using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping_Appilication.Models;

namespace Shopping_Appilication.Configurations
{
    public class CartDetailsConfiguration : IEntityTypeConfiguration<CartDetail>
    {
        public void Configure(EntityTypeBuilder<CartDetail> builder)
        {
            builder.ToTable("CartDetails");
            builder.HasKey(x => x.Id);
            builder.Property(c => c.Quantity).HasColumnType("int").
                IsRequired();
            builder.HasOne(c => c.Product).WithMany(c => c.CartDetails).
                HasForeignKey(c => c.IDSP).HasConstraintName("FK_Product");
            builder.HasOne(c => c.Cart).WithMany(c => c.CartDetails).
                HasForeignKey(c => c.UserID).HasConstraintName("FK_Cart");

        }
    }
}
