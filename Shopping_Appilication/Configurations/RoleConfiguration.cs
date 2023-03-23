using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping_Appilication.Models;

namespace Shopping_Appilication.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Role");
            builder.HasKey(c => c.RoleID);
            builder.Property(c => c.RoleName).HasColumnType("nvarchar(100)").
                IsRequired();
            builder.Property(c => c.Description).HasColumnType("nvarchar(1000)").
                IsRequired();
            builder.Property(c => c.Status).HasColumnType("nvarchar(500)").
                IsRequired();
        }
    }
}
