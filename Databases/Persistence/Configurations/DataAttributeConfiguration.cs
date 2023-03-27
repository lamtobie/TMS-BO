using Databases.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Databases.Persistence.Configurations
{
    public class DataAttributeConfiguration : IEntityTypeConfiguration<DataAttribute>
    {
        public void Configure(EntityTypeBuilder<DataAttribute> builder)
        {
            string tableName = "data_attribute";
            builder.ToTable(tableName);
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Code).HasColumnName("code");
            builder.Property(e => e.Name).HasColumnName("name");
            builder.Property(e => e.NameVI).HasColumnName("name_vi");
            builder.Property(e => e.NameEN).HasColumnName("name_en");
            builder.Property(e => e.DataType).HasColumnName("data_type");
            builder.Property(e => e.DataValue).HasColumnName("data_value");
            builder.Property(e => e.Metadata).HasColumnName("metadata").HasColumnType("jsonb");
            builder.Property(e => e.CreatedAt).HasColumnName("created_at");
            builder.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            builder.Property(e => e.CreatedBy).HasColumnName("created_by");
            builder.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            builder.Ignore(e => e.Key);
        }
    }
}