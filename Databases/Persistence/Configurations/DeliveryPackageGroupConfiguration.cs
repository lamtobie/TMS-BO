﻿using Databases;
using Databases.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Databases.Persistence.Configurations
{
    public class DeliveryPackageGroupConfiguration : IEntityTypeConfiguration<DeliveryPackageGroup>
    {
        public void Configure(EntityTypeBuilder<DeliveryPackageGroup> builder)
        {
            string tableName = "delivery_package_group";
            builder.ToTable(tableName);
            builder.HasKey(e => e.Code);
            builder.Property(e => e.Code).HasColumnName("code");
            builder.Property(e => e.Status).HasDefaultValue("Draft").HasColumnName("status");
            builder.Property(e => e.ParentCode).IsRequired(false).HasColumnName("parent_code");
            builder.Property(e => e.DeliveryOrderCode).HasColumnName("delivery_order_code");
            builder.Property(e => e.CreatedAt).HasColumnName("created_at");
            builder.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            builder.Property(e => e.CreatedBy).HasColumnName("created_by");
            builder.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            builder.Ignore(e => e.Key);

            builder.HasOne(e => e.Parent).WithMany(d => d.Childrens).HasForeignKey(e => e.ParentCode);
        }
    }
}