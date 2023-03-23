using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping_Appilication.Models;

namespace Shopping_Appilication.Configurations
{
    public class SizeConfiguration : IEntityTypeConfiguration<Size>
    {
        public void Configure(EntityTypeBuilder<Size> builder)
        {
            builder.ToTable("Size");
            builder.HasKey(c => c.IdSize);
            builder.Property(c => c.Name).HasColumnType("nvarchar(100)").
                IsRequired();
            builder.Property(c => c.Status).HasColumnType("int").
                IsRequired();
        }
    }
}
