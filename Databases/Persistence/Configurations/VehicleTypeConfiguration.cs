using Databases.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Databases.Persistence.Configurations;

public class VehicleTypeConfiguration : IEntityTypeConfiguration<VehicleType>
{
    public void Configure(EntityTypeBuilder<VehicleType> builder)
    {
        string table = "vehicle_type";
        builder.ToTable(table);
        builder.HasKey(e => e.Code);
        builder.Property(e => e.Code).HasColumnName("code");
        builder.Property(e => e.Name).HasColumnName("name");
        builder.Property(e => e.Length).HasColumnName("length");
        builder.Property(e => e.Height).HasColumnName("height");
        builder.Property(e => e.Width).HasColumnName("width");
        builder.Property(e => e.MaximumPayload).HasColumnName("maximum_payload");
        builder.Property(e => e.MaximumCapacity).HasColumnName("maximum_capacity");
        builder.Property(e => e.Status).HasColumnName("status").HasDefaultValue("Active");
        builder.Property(e => e.CreatedAt).HasColumnName("created_at");
        builder.Property(e => e.UpdatedAt).HasColumnName("updated_at");
        builder.Property(e => e.CreatedBy).HasColumnName("created_by");
        builder.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        builder.Ignore(e => e.Key);
    }
}