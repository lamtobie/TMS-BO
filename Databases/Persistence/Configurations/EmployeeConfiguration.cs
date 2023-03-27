using Databases.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Databases.Persistence.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            string tableName = "employee";
            builder.ToTable(tableName);
            builder.HasKey(e => e.Code);
            builder.Property(e => e.Code).HasColumnName("code");
            builder.Property(e => e.EmployeeType).HasColumnName("employee_type");
            builder.Property(e => e.FullName).HasColumnName("fullname");
            builder.Property(e => e.MobilePhone).HasColumnName("mobile_phone");
            builder.Property(e => e.Email).HasColumnName("email");
            builder.Property(e => e.Password).HasColumnName("password");
            builder.Property(e => e.AddressId).HasColumnName("address_id");
            builder.Property(e => e.IdentityNumber).HasColumnName("identity_number");
            builder.Property(e => e.IsStationAdmin).HasColumnName("is_station_admin");
            builder.Property(e => e.StationCode).HasColumnName("station_code");
            builder.Property(e => e.ThreePLTeam).HasColumnName("threepl_team");
            builder.Property(e => e.AvatarPicture).HasColumnName("avatar_picture");
            builder.Property(e => e.DrivingLicensePicture).HasColumnName("driving_license_picture");
            builder.Property(e => e.IdentityNumberPicture).HasColumnName("identity_number_picture");
            builder.Property(e => e.Status).HasDefaultValue("Draft").HasColumnName("status");
            builder.Property(e => e.CreatedAt).HasColumnName("created_at");
            builder.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            builder.Property(e => e.CreatedBy).HasColumnName("created_by");
            builder.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            builder.Ignore(e => e.Key);

            builder.HasOne(e => e.Station).WithMany(s => s.Employees).HasForeignKey(e => e.StationCode);
            builder.HasOne(e => e.Address).WithMany(e => e.Employees).HasForeignKey(e => e.AddressId);
        }
    }
}