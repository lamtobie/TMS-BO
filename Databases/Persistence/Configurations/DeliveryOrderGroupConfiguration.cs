using Databases.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Databases.Persistence.Configurations;

public class DeliveryOrderGroupConfiguration : IEntityTypeConfiguration<DeliveryOrderGroup>
{
    public void Configure(EntityTypeBuilder<DeliveryOrderGroup> builder)
    {
        string table = "delivery_order_group";
        builder.ToTable(table);
        builder.HasKey(e => e.Code);
        builder.Property(e => e.Code).HasColumnName("code");
        builder.Property(e => e.Status).HasColumnName("status");
        builder.Property(e => e.CancelReason).HasColumnName("cancel_reason");
        builder.Ignore(e => e.Key);
        builder.Property(e => e.CreatedAt).HasColumnName("created_at");
        builder.Property(e => e.UpdatedAt).HasColumnName("updated_at");
        builder.Property(e => e.CreatedBy).HasColumnName("created_by");
        builder.Property(e => e.UpdatedBy).HasColumnName("updated_by");
    }
}