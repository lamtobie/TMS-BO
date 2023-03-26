using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMS_FinalProject.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CreatedAt",
                table: "station",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "station",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "station",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedAt",
                table: "station",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedBy",
                table: "station",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatedAt",
                table: "employee",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "employee",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "employee",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedAt",
                table: "employee",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedBy",
                table: "employee",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatedAt",
                table: "delivery_route_segment",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "delivery_route_segment",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Key",
                table: "delivery_route_segment",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedAt",
                table: "delivery_route_segment",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedBy",
                table: "delivery_route_segment",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatedAt",
                table: "delivery_route",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "delivery_route",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Key",
                table: "delivery_route",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedAt",
                table: "delivery_route",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedBy",
                table: "delivery_route",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatedAt",
                table: "delivery_package_group",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "delivery_package_group",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "delivery_package_group",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedAt",
                table: "delivery_package_group",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedBy",
                table: "delivery_package_group",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatedAt",
                table: "delivery_package",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "delivery_package",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "delivery_package",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedAt",
                table: "delivery_package",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedBy",
                table: "delivery_package",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatedAt",
                table: "delivery_order_line",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "delivery_order_line",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Key",
                table: "delivery_order_line",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<long>(
                name: "UpdatedAt",
                table: "delivery_order_line",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedBy",
                table: "delivery_order_line",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatedAt",
                table: "delivery_order",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "delivery_order",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "delivery_order",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedAt",
                table: "delivery_order",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedBy",
                table: "delivery_order",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatedAt",
                table: "data_attribute",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "data_attribute",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Key",
                table: "data_attribute",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<long>(
                name: "UpdatedAt",
                table: "data_attribute",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedBy",
                table: "data_attribute",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "station");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "station");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "station");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "station");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "station");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "employee");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "employee");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "employee");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "employee");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "employee");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "delivery_route_segment");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "delivery_route_segment");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "delivery_route_segment");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "delivery_route_segment");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "delivery_route_segment");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "delivery_route");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "delivery_route");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "delivery_route");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "delivery_route");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "delivery_route");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "delivery_package_group");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "delivery_package_group");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "delivery_package_group");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "delivery_package_group");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "delivery_package_group");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "delivery_package");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "delivery_package");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "delivery_package");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "delivery_package");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "delivery_package");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "delivery_order_line");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "delivery_order_line");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "delivery_order_line");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "delivery_order_line");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "delivery_order_line");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "delivery_order");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "delivery_order");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "delivery_order");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "delivery_order");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "delivery_order");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "data_attribute");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "data_attribute");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "data_attribute");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "data_attribute");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "data_attribute");
        }
    }
}
