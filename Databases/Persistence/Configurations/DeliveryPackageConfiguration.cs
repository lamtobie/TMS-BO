using Databases;
using Databases.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Databases.Persistence.Configurations
{
    public class DeliveryPackageConfiguration : IEntityTypeConfiguration<DeliveryPackage>
    {
        public void Configure(EntityTypeBuilder<DeliveryPackage> builder)
        {
            string tableName = "delivery_package";
            builder.ToTable(tableName);
            builder.HasKey(e => e.Code);
            builder.Property(e => e.Code).HasColumnName("code");
            builder.Property(e => e.Status).HasDefaultValue("Draft").HasColumnName("status");
            builder.Property(e => e.ExternalCode).HasColumnName("external_code");
            builder.Property(e => e.ExternalSOCode).HasColumnName("external_so_code");
            builder.Property(e => e.Name).HasColumnName("name");
            builder.Property(e => e.Uom).HasColumnName("uom");
            builder.Property(e => e.CreatedAt).HasColumnName("created_at");
            builder.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            builder.Property(e => e.CreatedBy).HasColumnName("created_by");
            builder.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            builder.Ignore(e => e.Key);
        }
    }
}