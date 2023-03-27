using Databases.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Databases.Persistence.Configurations
{
    public class DeliverySessionConfiguration : IEntityTypeConfiguration<DeliverySession>
    {
        public void Configure(EntityTypeBuilder<DeliverySession> builder)
        {
            string tableName = "delivery_session";
            builder.ToTable(tableName);
            builder.HasKey(e => e.Code);
            builder.Property(e => e.Code).HasColumnName("code");
            builder.Property(e => e.SessionType).HasColumnName("session_type");
            builder.Property(e => e.ParentCode).IsRequired(false).HasColumnName("parent_code");
            builder.Property(e => e.DriverCode).IsRequired(false).HasColumnName("driver_code");
            builder.Property(e => e.CoordinatorCode).IsRequired(false).HasColumnName("coordinator_code");
            builder.Property(e => e.VehicleCode).IsRequired(false).HasColumnName("vehicle_code");
            builder.Property(e => e.StartStationCode).IsRequired(false).HasColumnName("start_station_code");
            builder.Property(e => e.EndStationCode).IsRequired(false).HasColumnName("end_station_code");
            builder.Property(e => e.SessionGroupCode).IsRequired(false).HasColumnName("session_group_code");
            builder.Property(e => e.Evidence).HasColumnName("evidence");
            builder.Property(e => e.ToCustomer).HasColumnName("to_customer");
            builder.Property(e => e.Status).HasDefaultValue("Draft").HasColumnName("status");
            builder.Property(e => e.Note).HasColumnName("note");
            builder.Property(e => e.Excepted).HasColumnName("excepted");
            builder.Property(e => e.Note).HasColumnName("note");
            builder.Property(e => e.ReasonCancel).HasColumnName("reason_cancel");
            builder.Property(e => e.ReasonReject).HasColumnName("reason_reject");
            builder.Property(e => e.TotalReceivedItems).HasColumnName("total_received_items");
            builder.Property(e => e.CreatedAt).HasColumnName("created_at");
            builder.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            builder.Property(e => e.CreatedBy).HasColumnName("created_by");
            builder.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            builder.Ignore(e => e.Key);

            builder.HasOne(e => e.Parent).WithMany(d => d.Childrens).HasForeignKey(e => e.ParentCode);
            builder.HasOne(e => e.Driver).WithMany(d => d.DriverDeliverySessions).HasForeignKey(e => e.DriverCode);
            builder.HasOne(e => e.Coordinator).WithMany(d => d.CoordinatorDeliverySessions).HasForeignKey(e => e.CoordinatorCode);
            builder.HasOne(e => e.StartStation).WithMany(d => d.StartDeliverySessions).HasForeignKey(e => e.StartStationCode);
            builder.HasOne(e => e.EndStation).WithMany(d => d.EndDeliverySessions).HasForeignKey(e => e.EndStationCode);
            builder.HasOne(e => e.SessionGroup).WithMany(d => d.Sessions).HasForeignKey(e => e.SessionGroupCode);
        }
    }
}