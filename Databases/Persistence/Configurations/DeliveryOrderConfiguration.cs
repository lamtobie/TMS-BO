using Databases.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Databases.Persistence.Configurations
{
    public class DeliveryOrderConfiguration : IEntityTypeConfiguration<DeliveryOrder>
    {
        public void Configure(EntityTypeBuilder<DeliveryOrder> builder)
        {
            string tableName = "delivery_order";
            builder.ToTable(tableName);
            builder.HasKey(e => e.Code);
            builder.Property(e => e.Code).HasColumnName("code");
            builder.Property(e => e.IsToCustomer).HasColumnName("is_to_customer");
            builder.Property(e => e.Email).HasColumnName("email");
            builder.Property(e => e.Status).HasDefaultValue("Draft").HasColumnName("status");
            builder.Property(e => e.ExpectedStartTime).HasColumnName("expected_start_time");
            builder.Property(e => e.ExpectedArrivalTime).HasColumnName("expected_arrival_time");
            builder.Property(e => e.ExpectedTimeConsumed).HasColumnName("expected_time_consumed");
            builder.Property(e => e.ActualStartTime).HasColumnName("actual_start_time");
            builder.Property(e => e.ActualArrivalTime).HasColumnName("actual_arrival_time");
            builder.Property(e => e.ActualTimeConsumed).HasColumnName("actual_time_consumed");
            builder.Property(e => e.ParentCode).IsRequired(false).HasColumnName("parent_code");
            builder.Property(e => e.DriverCode).IsRequired(false).HasColumnName("driver_code");
            builder.Property(e => e.CoordinatorCode).IsRequired(false).HasColumnName("coordinator_code");
            builder.Property(e => e.GroupCode).IsRequired(false).HasColumnName("group_code");
            builder.Property(e => e.SessionCode).IsRequired(false).HasColumnName("session_code");
            builder.Property(e => e.DeliveryRouteSegmentId).IsRequired(false).HasColumnName("delivery_route_segment_id");
            builder.Property(e => e.SourceBy).HasDefaultValue("TMS").HasColumnName("source_by");
            builder.Property(e => e.ReferenceCode).HasColumnName("reference_code");
            builder.Property(e => e.ThreePLTeam).HasColumnName("threepl_team");
            builder.Property(e => e.ProductType).HasColumnName("product_type");
            builder.Property(e => e.Amount).HasColumnName("amount");
            builder.Property(e => e.Weight).HasColumnName("weight");
            builder.Property(e => e.CodAllowed).HasColumnName("cod_allowed");
            builder.Property(e => e.CodAmount).HasColumnName("cod_amount");
            builder.Property(e => e.CodMethod).HasColumnName("cod_method");
            builder.Property(e => e.StartAddress).HasColumnName("start_address");
            builder.Property(e => e.StartContactPerson).HasColumnName("start_contact_person");
            builder.Property(e => e.StartContactPhone).HasColumnName("start_contact_phone");
            builder.Property(e => e.StartNote).HasColumnName("start_note");
            builder.Property(e => e.EndAddress).HasColumnName("end_address");
            builder.Property(e => e.EndContactPerson).HasColumnName("end_contact_person");
            builder.Property(e => e.EndContactPhone).HasColumnName("end_contact_phone");
            builder.Property(e => e.EndNote).HasColumnName("end_note");
            builder.Property(e => e.Additional).HasColumnName("additional").HasColumnType("jsonb");

            builder.HasOne(e => e.Parent).WithMany(d => d.Childrens).HasForeignKey(e => e.ParentCode);
            builder.HasOne(e => e.Driver).WithMany(d => d.DeliveryOrders).HasForeignKey(e => e.DriverCode);
            builder.HasOne(e => e.DeliveryRouteSegment);
        }
    }
}