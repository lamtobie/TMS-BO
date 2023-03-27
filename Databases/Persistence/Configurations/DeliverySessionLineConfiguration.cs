using Databases.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Databases.Persistence.Configurations
{
    public class DeliverySessionLineConfiguration : IEntityTypeConfiguration<DeliverySessionLine>
    {
        public void Configure(EntityTypeBuilder<DeliverySessionLine> builder)
        {
            string tableName = "delivery_session_line";
            builder.ToTable(tableName);
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Code).HasColumnName("code");
            builder.Property(e => e.DeliverySessionCode).HasColumnName("delivery_session_code");
            builder.Property(e => e.DeliveryOrderGroupCode).IsRequired(false).HasColumnName("delivery_order_group_code");
            builder.Property(e => e.DeliveryOrderParentCode).IsRequired(false).HasColumnName("delivery_order_parent_code");
            builder.Property(e => e.DeliveryOrderCode).HasColumnName("delivery_order_code");
            builder.Property(e => e.ReferenceCode).IsRequired(false).HasColumnName("reference_code");
            builder.Property(e => e.DeliveryPackageGroupCode).IsRequired(false).HasColumnName("delivery_package_group_code");
            builder.Property(e => e.DeliveryPackageCode).HasColumnName("delivery_package_code");
            builder.Property(e => e.Status).HasDefaultValue("Draft").HasColumnName("status");
            builder.Property(e => e.ConsumedAt).HasColumnName("consumed_at");
            builder.Property(e => e.ConsumedBy).HasColumnName("consumed_by");
            builder.Property(e => e.CreatedAt).HasColumnName("created_at");
            builder.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            builder.Property(e => e.CreatedBy).HasColumnName("created_by");
            builder.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            builder.Ignore(e => e.Key);

            builder.HasOne(e => e.DeliverySession).WithMany(d => d.DeliverySessionLines).HasForeignKey(e => e.DeliverySessionCode);
        }
    }
}