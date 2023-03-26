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
            builder.Property(e => e.DeliveryPackageGroupCode).IsRequired(false).HasColumnName("delivery_package_group_code");
            builder.Property(e => e.ExternalCode).HasColumnName("external_code");
            builder.Property(e => e.Name).HasColumnName("name");
            builder.Property(e => e.Uom).HasColumnName("uom");

            builder.HasOne(e => e.DeliveryPackageGroup).WithMany(d => d.DeliveryPackages).HasForeignKey(e => e.DeliveryPackageGroupCode);
        }
    }
}