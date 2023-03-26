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
            builder.Property(e => e.Address).HasColumnName("address");
            builder.Property(e => e.Lat).HasColumnName("lat");
            builder.Property(e => e.Long).HasColumnName("long");
            builder.Property(e => e.Status).HasDefaultValue("Draft").HasColumnName("status");
        }
    }
}