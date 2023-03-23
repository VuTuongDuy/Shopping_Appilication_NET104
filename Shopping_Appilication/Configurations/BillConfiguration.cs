using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping_Appilication.Models;

namespace Shopping_Appilication.Configurations
{
    public class BillConfiguration : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.ToTable("HoaDon");// Đặt tên cho bảng
            builder.HasKey(c => c.Id); // Tạo khóa chính
            // Set các thuộc tính
            builder.Property(c => c.CreateDate).HasColumnType("Datetime").
                IsRequired(); // Datetime not null
            builder.Property(c => c.Status).HasColumnType("nvarchar(1000)").
                IsRequired();
            builder.HasOne(c => c.User).WithMany(c => c.Bills).
                HasForeignKey(c => c.UserID);
        }
    }
}
