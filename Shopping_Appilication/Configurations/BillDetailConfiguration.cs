using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping_Appilication.Models;

namespace Shopping_Appilication.Configurations
{
    public class BillDetailConfiguration : IEntityTypeConfiguration<BillDetail>
    {
        public void Configure(EntityTypeBuilder<BillDetail> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Price).HasColumnType("int");
            builder.Property(c => c.Quantity).HasColumnType("int");
            builder.HasOne(c => c.Bill).WithMany(v => v.BillDetails).
                HasForeignKey(x => x.IdHD);
            builder.HasOne(c => c.Product).WithMany(v => v.BillDetails).
                HasForeignKey(x => x.IdSP);
        }
    }
}
