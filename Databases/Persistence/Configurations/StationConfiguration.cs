using Databases.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Databases.Persistence.Configurations
{
    public class StationConfiguration : IEntityTypeConfiguration<Station>
    {
        public void Configure(EntityTypeBuilder<Station> builder)
        {
            string tableName = "station";
            builder.ToTable(tableName);
            builder.HasKey(e => e.Code);
            builder.Property(e => e.Code).HasColumnName("code");
            builder.Property(e => e.Name).HasColumnName("name");
            builder.Property(e => e.ContactPerson).HasColumnName("contact_person");
            builder.Property(e => e.ContactEmail).HasColumnName("contact_email");
            builder.Property(e => e.ContactPhone).HasColumnName("contact_phone");
            builder.Property(e => e.ContactPersonAnother).HasColumnName("contact_person_another");
            builder.Property(e => e.ContactEmailAnother).HasColumnName("contact_email_another");
            builder.Property(e => e.ContactPhoneAnother).HasColumnName("contact_phone_another");
            builder.Property(e => e.AddressId).HasColumnName("address_id");
            builder.Property(e => e.Status).HasDefaultValue("Draft").HasColumnName("status");
            builder.Property(e => e.CreatedAt).HasColumnName("created_at");
            builder.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            builder.Property(e => e.CreatedBy).HasColumnName("created_by");
            builder.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            builder.Ignore(e => e.Key);

            builder.HasOne(e => e.Address).WithMany(e => e.Stations).HasForeignKey(e => e.AddressId);
        }
    }
}
