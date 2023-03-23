using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping_Appilication.Models;

namespace Shopping_Appilication.Configurations
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Cart");
            builder.HasKey(c => c.UserID);
            builder.Property(c => c.Description).HasColumnType("nvarchar(1000)");
        }
    }
}
