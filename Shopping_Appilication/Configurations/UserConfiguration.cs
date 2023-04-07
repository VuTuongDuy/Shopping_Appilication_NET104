using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping_Appilication.Models;

namespace Shopping_Appilication.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(c => c.UserID);
            builder.Property(c => c.UserName).HasColumnType("nvarchar(150)").
               IsRequired();
            builder.Property(c => c.Password).HasColumnType("nvarchar(100)").
               IsRequired();
            builder.Property(c => c.Email).HasColumnType("nvarchar(200)").
                IsRequired();
            builder.Property(c => c.ResetPasswordToken).HasColumnType("nvarchar(200)");
            builder.Property(c => c.Status).HasColumnType("int").
               IsRequired();
            builder.HasOne(x => x.Role).WithMany(v => v.Users).
                HasForeignKey(c => c.RoleID);
        }
    }
}
