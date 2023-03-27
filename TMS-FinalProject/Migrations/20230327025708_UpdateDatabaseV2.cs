using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMS_FinalProject.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_delivery_order_delivery_route_segment_delivery_route_segmen~",
                table: "delivery_order");

            migrationBuilder.DropForeignKey(
                name: "FK_delivery_package_delivery_package_group_delivery_package_gr~",
                table: "delivery_package");

            migrationBuilder.DropForeignKey(
                name: "FK_delivery_package_group_delivery_order_delivery_order_code",
                table: "delivery_package_group");

            migrationBuilder.DropIndex(
                name: "IX_delivery_order_delivery_route_segment_id",
                table: "delivery_order");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "station");

            migrationBuilder.DropColumn(
                name: "address",
                table: "station");

            migrationBuilder.DropColumn(
                name: "lat",
                table: "station");

            migrationBuilder.DropColumn(
                name: "long",
                table: "station");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "employee");

            migrationBuilder.DropColumn(
                name: "address",
                table: "employee");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "delivery_route_segment");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "delivery_route");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "delivery_package_group");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "delivery_order_line");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "data_attribute");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "station",
                newName: "updated_by");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "station",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "station",
                newName: "created_by");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "station",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "employee",
                newName: "updated_by");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "employee",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "employee",
                newName: "created_by");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "employee",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "delivery_route_segment",
                newName: "updated_by");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "delivery_route_segment",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "delivery_route_segment",
                newName: "created_by");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "delivery_route_segment",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "delivery_route",
                newName: "updated_by");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "delivery_route",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "delivery_route",
                newName: "created_by");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "delivery_route",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "delivery_package_group",
                newName: "updated_by");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "delivery_package_group",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "delivery_package_group",
                newName: "created_by");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "delivery_package_group",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "delivery_package_group_code",
                table: "delivery_package",
                newName: "DeliveryPackageGroupCode");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "delivery_package",
                newName: "updated_by");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "delivery_package",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "delivery_package",
                newName: "created_by");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "delivery_package",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "Key",
                table: "delivery_package",
                newName: "external_so_code");

            migrationBuilder.RenameIndex(
                name: "IX_delivery_package_delivery_package_group_code",
                table: "delivery_package",
                newName: "IX_delivery_package_DeliveryPackageGroupCode");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "delivery_order_line",
                newName: "updated_by");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "delivery_order_line",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "delivery_order_line",
                newName: "created_by");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "delivery_order_line",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "delivery_order",
                newName: "updated_by");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "delivery_order",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "delivery_order",
                newName: "created_by");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "delivery_order",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "start_address",
                table: "delivery_order",
                newName: "start_station_code");

            migrationBuilder.RenameColumn(
                name: "end_address",
                table: "delivery_order",
                newName: "return_address");

            migrationBuilder.RenameColumn(
                name: "amount",
                table: "delivery_order",
                newName: "total_items");

            migrationBuilder.RenameColumn(
                name: "Key",
                table: "delivery_order",
                newName: "reason");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "data_attribute",
                newName: "updated_by");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "data_attribute",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "data_attribute",
                newName: "created_by");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "data_attribute",
                newName: "created_at");

            migrationBuilder.AddColumn<Guid>(
                name: "address_id",
                table: "station",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "address_id",
                table: "employee",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "delivery_order_code",
                table: "delivery_package_group",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "source_by",
                table: "delivery_order",
                type: "text",
                nullable: true,
                defaultValue: "TMS Client",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldDefaultValue: "TMS");

            migrationBuilder.AddColumn<bool>(
                name: "cod_received",
                table: "delivery_order",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "end_address_id",
                table: "delivery_order",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "end_station_code",
                table: "delivery_order",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "evidence",
                table: "delivery_order",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "note",
                table: "delivery_order",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "number_of_transit",
                table: "delivery_order",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "start_address_id",
                table: "delivery_order",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "transit_order",
                table: "delivery_order",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "address",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    block_address = table.Column<string>(type: "text", nullable: true),
                    cluster_address = table.Column<string>(type: "text", nullable: true),
                    quarter_address = table.Column<string>(type: "text", nullable: true),
                    sub_quarter_address = table.Column<string>(type: "text", nullable: true),
                    text = table.Column<string>(type: "text", nullable: false),
                    slic_code = table.Column<string>(type: "text", nullable: false),
                    slic_label = table.Column<string>(type: "text", nullable: false),
                    lat = table.Column<decimal>(type: "numeric", nullable: false),
                    @long = table.Column<decimal>(name: "long", type: "numeric", nullable: false),
                    slic_region = table.Column<string>(type: "text", nullable: false),
                    slic_level = table.Column<string>(type: "text", nullable: false),
                    slic_ward = table.Column<string>(type: "text", nullable: false),
                    slic_district = table.Column<string>(type: "text", nullable: false),
                    slic_province = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<long>(type: "bigint", nullable: false),
                    updated_at = table.Column<long>(type: "bigint", nullable: true),
                    created_by = table.Column<long>(type: "bigint", nullable: true),
                    updated_by = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_address", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "delivery_order_group",
                columns: table => new
                {
                    code = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    cancel_reason = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<long>(type: "bigint", nullable: false),
                    updated_at = table.Column<long>(type: "bigint", nullable: true),
                    created_by = table.Column<long>(type: "bigint", nullable: true),
                    updated_by = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_delivery_order_group", x => x.code);
                });

            migrationBuilder.CreateTable(
                name: "delivery_session_group",
                columns: table => new
                {
                    code = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<long>(type: "bigint", nullable: false),
                    updated_at = table.Column<long>(type: "bigint", nullable: true),
                    created_by = table.Column<long>(type: "bigint", nullable: true),
                    updated_by = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_delivery_session_group", x => x.code);
                });

            migrationBuilder.CreateTable(
                name: "vehicle_type",
                columns: table => new
                {
                    code = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    length = table.Column<float>(type: "real", nullable: true),
                    height = table.Column<float>(type: "real", nullable: true),
                    width = table.Column<float>(type: "real", nullable: true),
                    maximum_payload = table.Column<float>(type: "real", nullable: true),
                    maximum_capacity = table.Column<float>(type: "real", nullable: true),
                    status = table.Column<string>(type: "text", nullable: false, defaultValue: "Active"),
                    created_at = table.Column<long>(type: "bigint", nullable: false),
                    updated_at = table.Column<long>(type: "bigint", nullable: true),
                    created_by = table.Column<long>(type: "bigint", nullable: true),
                    updated_by = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicle_type", x => x.code);
                });

            migrationBuilder.CreateTable(
                name: "vehicle",
                columns: table => new
                {
                    code = table.Column<string>(type: "text", nullable: false),
                    number_plate = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false, defaultValue: "free"),
                    vehicle_type_code = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<long>(type: "bigint", nullable: false),
                    updated_at = table.Column<long>(type: "bigint", nullable: true),
                    created_by = table.Column<long>(type: "bigint", nullable: true),
                    updated_by = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicle", x => x.code);
                    table.ForeignKey(
                        name: "FK_vehicle_vehicle_type_vehicle_type_code",
                        column: x => x.vehicle_type_code,
                        principalTable: "vehicle_type",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "delivery_session",
                columns: table => new
                {
                    code = table.Column<string>(type: "text", nullable: false),
                    session_type = table.Column<string>(type: "text", nullable: true),
                    parent_code = table.Column<string>(type: "text", nullable: true),
                    driver_code = table.Column<string>(type: "text", nullable: true),
                    coordinator_code = table.Column<string>(type: "text", nullable: true),
                    vehicle_code = table.Column<string>(type: "text", nullable: true),
                    start_station_code = table.Column<string>(type: "text", nullable: true),
                    end_station_code = table.Column<string>(type: "text", nullable: true),
                    session_group_code = table.Column<string>(type: "text", nullable: true),
                    to_customer = table.Column<bool>(type: "boolean", nullable: true),
                    status = table.Column<string>(type: "text", nullable: false, defaultValue: "Draft"),
                    note = table.Column<string>(type: "text", nullable: true),
                    evidence = table.Column<string>(type: "text", nullable: true),
                    excepted = table.Column<string>(type: "text", nullable: true),
                    reason_cancel = table.Column<string>(type: "text", nullable: true),
                    reason_reject = table.Column<string>(type: "text", nullable: true),
                    total_received_items = table.Column<int>(type: "integer", nullable: true),
                    created_at = table.Column<long>(type: "bigint", nullable: false),
                    updated_at = table.Column<long>(type: "bigint", nullable: true),
                    created_by = table.Column<long>(type: "bigint", nullable: true),
                    updated_by = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_delivery_session", x => x.code);
                    table.ForeignKey(
                        name: "FK_delivery_session_delivery_session_group_session_group_code",
                        column: x => x.session_group_code,
                        principalTable: "delivery_session_group",
                        principalColumn: "code");
                    table.ForeignKey(
                        name: "FK_delivery_session_delivery_session_parent_code",
                        column: x => x.parent_code,
                        principalTable: "delivery_session",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_delivery_session_employee_coordinator_code",
                        column: x => x.coordinator_code,
                        principalTable: "employee",
                        principalColumn: "code");
                    table.ForeignKey(
                        name: "FK_delivery_session_employee_driver_code",
                        column: x => x.driver_code,
                        principalTable: "employee",
                        principalColumn: "code");
                    table.ForeignKey(
                        name: "FK_delivery_session_station_end_station_code",
                        column: x => x.end_station_code,
                        principalTable: "station",
                        principalColumn: "code");
                    table.ForeignKey(
                        name: "FK_delivery_session_station_start_station_code",
                        column: x => x.start_station_code,
                        principalTable: "station",
                        principalColumn: "code");
                    table.ForeignKey(
                        name: "FK_delivery_session_vehicle_vehicle_code",
                        column: x => x.vehicle_code,
                        principalTable: "vehicle",
                        principalColumn: "code");
                });

            migrationBuilder.CreateTable(
                name: "delivery_session_line",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    delivery_session_code = table.Column<string>(type: "text", nullable: false),
                    delivery_order_group_code = table.Column<string>(type: "text", nullable: true),
                    delivery_order_parent_code = table.Column<string>(type: "text", nullable: true),
                    DeliveryOrderChildrenCode = table.Column<string>(type: "text", nullable: true),
                    delivery_order_code = table.Column<string>(type: "text", nullable: true),
                    reference_code = table.Column<string>(type: "text", nullable: true),
                    delivery_package_group_code = table.Column<string>(type: "text", nullable: true),
                    delivery_package_code = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<string>(type: "text", nullable: false, defaultValue: "Draft"),
                    consumed_at = table.Column<long>(type: "bigint", nullable: true),
                    consumed_by = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<long>(type: "bigint", nullable: false),
                    updated_at = table.Column<long>(type: "bigint", nullable: true),
                    created_by = table.Column<long>(type: "bigint", nullable: true),
                    updated_by = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_delivery_session_line", x => x.id);
                    table.ForeignKey(
                        name: "FK_delivery_session_line_delivery_session_delivery_session_code",
                        column: x => x.delivery_session_code,
                        principalTable: "delivery_session",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_station_address_id",
                table: "station",
                column: "address_id");

            migrationBuilder.CreateIndex(
                name: "IX_employee_address_id",
                table: "employee",
                column: "address_id");

            migrationBuilder.CreateIndex(
                name: "IX_delivery_order_end_address_id",
                table: "delivery_order",
                column: "end_address_id");

            migrationBuilder.CreateIndex(
                name: "IX_delivery_order_group_code",
                table: "delivery_order",
                column: "group_code");

            migrationBuilder.CreateIndex(
                name: "IX_delivery_order_session_code",
                table: "delivery_order",
                column: "session_code");

            migrationBuilder.CreateIndex(
                name: "IX_delivery_order_start_address_id",
                table: "delivery_order",
                column: "start_address_id");

            migrationBuilder.CreateIndex(
                name: "IX_delivery_session_coordinator_code",
                table: "delivery_session",
                column: "coordinator_code");

            migrationBuilder.CreateIndex(
                name: "IX_delivery_session_driver_code",
                table: "delivery_session",
                column: "driver_code");

            migrationBuilder.CreateIndex(
                name: "IX_delivery_session_end_station_code",
                table: "delivery_session",
                column: "end_station_code");

            migrationBuilder.CreateIndex(
                name: "IX_delivery_session_parent_code",
                table: "delivery_session",
                column: "parent_code");

            migrationBuilder.CreateIndex(
                name: "IX_delivery_session_session_group_code",
                table: "delivery_session",
                column: "session_group_code");

            migrationBuilder.CreateIndex(
                name: "IX_delivery_session_start_station_code",
                table: "delivery_session",
                column: "start_station_code");

            migrationBuilder.CreateIndex(
                name: "IX_delivery_session_vehicle_code",
                table: "delivery_session",
                column: "vehicle_code");

            migrationBuilder.CreateIndex(
                name: "IX_delivery_session_line_delivery_session_code",
                table: "delivery_session_line",
                column: "delivery_session_code");

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_vehicle_type_code",
                table: "vehicle",
                column: "vehicle_type_code");

            migrationBuilder.AddForeignKey(
                name: "FK_delivery_order_address_end_address_id",
                table: "delivery_order",
                column: "end_address_id",
                principalTable: "address",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_delivery_order_address_start_address_id",
                table: "delivery_order",
                column: "start_address_id",
                principalTable: "address",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_delivery_order_delivery_order_group_group_code",
                table: "delivery_order",
                column: "group_code",
                principalTable: "delivery_order_group",
                principalColumn: "code");

            migrationBuilder.AddForeignKey(
                name: "FK_delivery_order_delivery_session_session_code",
                table: "delivery_order",
                column: "session_code",
                principalTable: "delivery_session",
                principalColumn: "code");

            migrationBuilder.AddForeignKey(
                name: "FK_delivery_package_delivery_package_group_DeliveryPackageGrou~",
                table: "delivery_package",
                column: "DeliveryPackageGroupCode",
                principalTable: "delivery_package_group",
                principalColumn: "code");

            migrationBuilder.AddForeignKey(
                name: "FK_delivery_package_group_delivery_order_delivery_order_code",
                table: "delivery_package_group",
                column: "delivery_order_code",
                principalTable: "delivery_order",
                principalColumn: "code");

            migrationBuilder.AddForeignKey(
                name: "FK_employee_address_address_id",
                table: "employee",
                column: "address_id",
                principalTable: "address",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_station_address_address_id",
                table: "station",
                column: "address_id",
                principalTable: "address",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_delivery_order_address_end_address_id",
                table: "delivery_order");

            migrationBuilder.DropForeignKey(
                name: "FK_delivery_order_address_start_address_id",
                table: "delivery_order");

            migrationBuilder.DropForeignKey(
                name: "FK_delivery_order_delivery_order_group_group_code",
                table: "delivery_order");

            migrationBuilder.DropForeignKey(
                name: "FK_delivery_order_delivery_session_session_code",
                table: "delivery_order");

            migrationBuilder.DropForeignKey(
                name: "FK_delivery_package_delivery_package_group_DeliveryPackageGrou~",
                table: "delivery_package");

            migrationBuilder.DropForeignKey(
                name: "FK_delivery_package_group_delivery_order_delivery_order_code",
                table: "delivery_package_group");

            migrationBuilder.DropForeignKey(
                name: "FK_employee_address_address_id",
                table: "employee");

            migrationBuilder.DropForeignKey(
                name: "FK_station_address_address_id",
                table: "station");

            migrationBuilder.DropTable(
                name: "address");

            migrationBuilder.DropTable(
                name: "delivery_order_group");

            migrationBuilder.DropTable(
                name: "delivery_session_line");

            migrationBuilder.DropTable(
                name: "delivery_session");

            migrationBuilder.DropTable(
                name: "delivery_session_group");

            migrationBuilder.DropTable(
                name: "vehicle");

            migrationBuilder.DropTable(
                name: "vehicle_type");

            migrationBuilder.DropIndex(
                name: "IX_station_address_id",
                table: "station");

            migrationBuilder.DropIndex(
                name: "IX_employee_address_id",
                table: "employee");

            migrationBuilder.DropIndex(
                name: "IX_delivery_order_end_address_id",
                table: "delivery_order");

            migrationBuilder.DropIndex(
                name: "IX_delivery_order_group_code",
                table: "delivery_order");

            migrationBuilder.DropIndex(
                name: "IX_delivery_order_session_code",
                table: "delivery_order");

            migrationBuilder.DropIndex(
                name: "IX_delivery_order_start_address_id",
                table: "delivery_order");

            migrationBuilder.DropColumn(
                name: "address_id",
                table: "station");

            migrationBuilder.DropColumn(
                name: "address_id",
                table: "employee");

            migrationBuilder.DropColumn(
                name: "cod_received",
                table: "delivery_order");

            migrationBuilder.DropColumn(
                name: "end_address_id",
                table: "delivery_order");

            migrationBuilder.DropColumn(
                name: "end_station_code",
                table: "delivery_order");

            migrationBuilder.DropColumn(
                name: "evidence",
                table: "delivery_order");

            migrationBuilder.DropColumn(
                name: "note",
                table: "delivery_order");

            migrationBuilder.DropColumn(
                name: "number_of_transit",
                table: "delivery_order");

            migrationBuilder.DropColumn(
                name: "start_address_id",
                table: "delivery_order");

            migrationBuilder.DropColumn(
                name: "transit_order",
                table: "delivery_order");

            migrationBuilder.RenameColumn(
                name: "updated_by",
                table: "station",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "station",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "created_by",
                table: "station",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "station",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "updated_by",
                table: "employee",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "employee",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "created_by",
                table: "employee",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "employee",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "updated_by",
                table: "delivery_route_segment",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "delivery_route_segment",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "created_by",
                table: "delivery_route_segment",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "delivery_route_segment",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "updated_by",
                table: "delivery_route",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "delivery_route",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "created_by",
                table: "delivery_route",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "delivery_route",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "updated_by",
                table: "delivery_package_group",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "delivery_package_group",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "created_by",
                table: "delivery_package_group",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "delivery_package_group",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "updated_by",
                table: "delivery_package",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "delivery_package",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "created_by",
                table: "delivery_package",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "delivery_package",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "DeliveryPackageGroupCode",
                table: "delivery_package",
                newName: "delivery_package_group_code");

            migrationBuilder.RenameColumn(
                name: "external_so_code",
                table: "delivery_package",
                newName: "Key");

            migrationBuilder.RenameIndex(
                name: "IX_delivery_package_DeliveryPackageGroupCode",
                table: "delivery_package",
                newName: "IX_delivery_package_delivery_package_group_code");

            migrationBuilder.RenameColumn(
                name: "updated_by",
                table: "delivery_order_line",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "delivery_order_line",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "created_by",
                table: "delivery_order_line",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "delivery_order_line",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "updated_by",
                table: "delivery_order",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "delivery_order",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "created_by",
                table: "delivery_order",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "delivery_order",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "total_items",
                table: "delivery_order",
                newName: "amount");

            migrationBuilder.RenameColumn(
                name: "start_station_code",
                table: "delivery_order",
                newName: "start_address");

            migrationBuilder.RenameColumn(
                name: "return_address",
                table: "delivery_order",
                newName: "end_address");

            migrationBuilder.RenameColumn(
                name: "reason",
                table: "delivery_order",
                newName: "Key");

            migrationBuilder.RenameColumn(
                name: "updated_by",
                table: "data_attribute",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "data_attribute",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "created_by",
                table: "data_attribute",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "data_attribute",
                newName: "CreatedAt");

            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "station",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "address",
                table: "station",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "lat",
                table: "station",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "long",
                table: "station",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "employee",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "address",
                table: "employee",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Key",
                table: "delivery_route_segment",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Key",
                table: "delivery_route",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "delivery_order_code",
                table: "delivery_package_group",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "delivery_package_group",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Key",
                table: "delivery_order_line",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "source_by",
                table: "delivery_order",
                type: "text",
                nullable: true,
                defaultValue: "TMS",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldDefaultValue: "TMS Client");

            migrationBuilder.AddColumn<Guid>(
                name: "Key",
                table: "data_attribute",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_delivery_order_delivery_route_segment_id",
                table: "delivery_order",
                column: "delivery_route_segment_id");

            migrationBuilder.AddForeignKey(
                name: "FK_delivery_order_delivery_route_segment_delivery_route_segmen~",
                table: "delivery_order",
                column: "delivery_route_segment_id",
                principalTable: "delivery_route_segment",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_delivery_package_delivery_package_group_delivery_package_gr~",
                table: "delivery_package",
                column: "delivery_package_group_code",
                principalTable: "delivery_package_group",
                principalColumn: "code");

            migrationBuilder.AddForeignKey(
                name: "FK_delivery_package_group_delivery_order_delivery_order_code",
                table: "delivery_package_group",
                column: "delivery_order_code",
                principalTable: "delivery_order",
                principalColumn: "code",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
