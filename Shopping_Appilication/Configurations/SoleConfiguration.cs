using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping_Appilication.Models;

namespace Shopping_Appilication.Configurations
{
    public class SoleConfiguration : IEntityTypeConfiguration<Sole>
    {
        public void Configure(EntityTypeBuilder<Sole> builder)
        {
            builder.ToTable("Sole");
            builder.HasKey(c => c.IdSole);
            builder.Property(c => c.Name).HasColumnType("nvarchar(100)").
                IsRequired();
            builder.Property(c => c.Fabric).HasColumnType("nvarchar(200)").
                IsRequired();
            builder.Property(c => c.Height).HasColumnType("int").
                IsRequired();
            builder.Property(c => c.Status).HasColumnType("int").
                IsRequired();
        }
    }
}
