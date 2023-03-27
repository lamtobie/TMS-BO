using Databases.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Databases.Persistence.Configurations
{
    public class DeliveryOrderLineConfiguration : IEntityTypeConfiguration<DeliveryOrderLine>
    {
        public void Configure(EntityTypeBuilder<DeliveryOrderLine> builder)
        {
            string tableName = "delivery_order_line";
            builder.ToTable(tableName);
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.DeliveryOrderCode).HasColumnName("delivery_order_code");
            builder.Property(e => e.DeliveryPackageCode).HasColumnName("delivery_package_code");
            builder.Property(e => e.Status).HasDefaultValue("Draft").HasColumnName("status");
            builder.Property(e => e.Quantity).HasColumnName("quantity");
            builder.Property(e => e.Weight).HasColumnName("weight");
            builder.Property(e => e.Length).HasColumnName("length");
            builder.Property(e => e.Width).HasColumnName("width");
            builder.Property(e => e.Height).HasColumnName("height");
            builder.Property(e => e.CreatedAt).HasColumnName("created_at");
            builder.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            builder.Property(e => e.CreatedBy).HasColumnName("created_by");
            builder.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            builder.Ignore(e => e.Key);

            builder.HasOne(e => e.DeliveryOrder).WithMany(d => d.DeliveryOrderLines).HasForeignKey(e => e.DeliveryOrderCode);
            builder.HasOne(e => e.DeliveryPackage).WithMany(d => d.DeliveryOrderLines).HasForeignKey(e => e.DeliveryPackageCode);
        }
    }
}