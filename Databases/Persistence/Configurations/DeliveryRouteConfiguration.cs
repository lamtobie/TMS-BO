using Databases;
using Databases.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Databases.Persistence.Configurations
{
    public class DeliveryRouteConfiguration : IEntityTypeConfiguration<DeliveryRoute>
    {
        public void Configure(EntityTypeBuilder<DeliveryRoute> builder)
        {
            string tableName = "delivery_route";
            builder.ToTable(tableName);
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Order).HasColumnName("order");
            builder.Property(e => e.Length).HasColumnName("length");
            builder.Property(e => e.ExpectedStartTime).HasColumnName("expected_start_time");
            builder.Property(e => e.ExpectedArrivalTime).HasColumnName("expected_arrival_time");
            builder.Property(e => e.ExpectedTimeConsumed).HasColumnName("expected_time_consumed");
            builder.Property(e => e.ActualStartTime).HasColumnName("actual_start_time");
            builder.Property(e => e.ActualArrivalTime).HasColumnName("actual_arrival_time");
            builder.Property(e => e.ActualTimeConsumed).HasColumnName("actual_time_consumed");
            builder.Property(e => e.StartStationId).IsRequired(false).HasColumnName("start_station_id");
            builder.Property(e => e.EndStationId).IsRequired(false).HasColumnName("snd_station_id");
            builder.Property(e => e.DriverCode).IsRequired(false).HasColumnName("driver_code");
            builder.Property(e => e.Status).HasDefaultValue("Draft").HasColumnName("status");
            builder.Property(e => e.CreatedAt).HasColumnName("created_at");
            builder.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            builder.Property(e => e.CreatedBy).HasColumnName("created_by");
            builder.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            builder.Ignore(e => e.Key);

            builder.HasOne(e => e.StartStation).WithMany(s => s.StartDeliveryRoutes).HasForeignKey(e => e.StartStationId);
            builder.HasOne(e => e.EndStation).WithMany(s => s.EndDeliveryRoutes).HasForeignKey(e => e.EndStationId);
            builder.HasOne(e => e.Driver).WithMany(d => d.DeliveryRoutes).HasForeignKey(e => e.DriverCode);
        }
    }
}