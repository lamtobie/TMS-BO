using Databases.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Databases.Persistence.Configurations;

public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        string table = "vehicle";
        builder.ToTable(table);
        builder.HasKey(e => e.Code);
        builder.Property(e => e.Code).HasColumnName("code");
        builder.Property(e => e.VehicleTypeCode).HasColumnName("vehicle_type_code");
        builder.Property(e => e.NumberPlate).HasColumnName("number_plate");
        builder.Property(e => e.Status).HasColumnName("status").HasDefaultValue("free");
        builder.Property(e => e.CreatedAt).HasColumnName("created_at");
        builder.Property(e => e.UpdatedAt).HasColumnName("updated_at");
        builder.Property(e => e.CreatedBy).HasColumnName("created_by");
        builder.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        builder.Ignore(e => e.Key);

        builder.HasOne(e => e.VehicleType).WithMany(e => e.Vehicles).HasForeignKey(e => e.VehicleTypeCode);
    }
}