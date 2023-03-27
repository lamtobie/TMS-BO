using Databases.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Databases.Persistence.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            string tableName = "address";
            builder.ToTable(tableName);
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.BlockAddress).IsRequired(false).HasColumnName("block_address");
            builder.Property(e => e.ClusterAddress).IsRequired(false).HasColumnName("cluster_address");
            builder.Property(e => e.QuarterAddress).IsRequired(false).HasColumnName("quarter_address");
            builder.Property(e => e.SubQuarterAddress).IsRequired(false).HasColumnName("sub_quarter_address");
            builder.Property(e => e.Text).HasColumnName("text");
            builder.Property(e => e.SlicCode).HasColumnName("slic_code");
            builder.Property(e => e.SlicLabel).HasColumnName("slic_label");
            builder.Property(e => e.Lat).HasColumnName("lat");
            builder.Property(e => e.Long).HasColumnName("long");
            builder.Property(e => e.SlicRegion).HasColumnName("slic_region");
            builder.Property(e => e.SlicLevel).HasColumnName("slic_level");
            builder.Property(e => e.SlicWard).HasColumnName("slic_ward");
            builder.Property(e => e.SlicDistrict).HasColumnName("slic_district");
            builder.Property(e => e.SlicProvince).HasColumnName("slic_province");
            builder.Property(e => e.CreatedAt).HasColumnName("created_at");
            builder.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            builder.Property(e => e.CreatedBy).HasColumnName("created_by");
            builder.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            builder.Ignore(e => e.Key);
        }
    }
}
