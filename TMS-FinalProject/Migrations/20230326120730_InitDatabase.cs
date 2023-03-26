using System;
using Databases.Entities;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TMS_FinalProject.Migrations
{
    /// <inheritdoc />
    public partial class InitDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "data_attribute",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    name_vi = table.Column<string>(type: "text", nullable: true),
                    name_en = table.Column<string>(type: "text", nullable: true),
                    data_type = table.Column<string>(type: "text", nullable: false),
                    data_value = table.Column<string>(type: "text", nullable: true),
                    metadata = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_data_attribute", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "station",
                columns: table => new
                {
                    code = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    contact_person = table.Column<string>(type: "text", nullable: false),
                    contact_email = table.Column<string>(type: "text", nullable: false),
                    contact_phone = table.Column<string>(type: "text", nullable: false),
                    contact_person_another = table.Column<string>(type: "text", nullable: true),
                    contact_email_another = table.Column<string>(type: "text", nullable: true),
                    contact_phone_another = table.Column<string>(type: "text", nullable: true),
                    address = table.Column<string>(type: "text", nullable: false),
                    lat = table.Column<decimal>(type: "numeric", nullable: true),
                    @long = table.Column<decimal>(name: "long", type: "numeric", nullable: true),
                    status = table.Column<string>(type: "text", nullable: false, defaultValue: "Draft")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_station", x => x.code);
                });

            migrationBuilder.CreateTable(
                name: "employee",
                columns: table => new
                {
                    code = table.Column<string>(type: "text", nullable: false),
                    employee_type = table.Column<string>(type: "text", nullable: false),
                    fullname = table.Column<string>(type: "text", nullable: false),
                    mobile_phone = table.Column<string>(type: "text", nullable: false),
                    station_code = table.Column<string>(type: "text", nullable: true),
                    is_station_admin = table.Column<bool>(type: "boolean", nullable: true),
                    identity_number = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    password = table.Column<string>(type: "text", nullable: true),
                    address = table.Column<string>(type: "text", nullable: true),
                    threepl_team = table.Column<string>(type: "text", nullable: true),
                    avatar_picture = table.Column<string>(type: "text", nullable: true),
                    driving_license_picture = table.Column<string>(type: "text", nullable: true),
                    identity_number_picture = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<string>(type: "text", nullable: false, defaultValue: "Draft")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee", x => x.code);
                    table.ForeignKey(
                        name: "FK_employee_station_station_code",
                        column: x => x.station_code,
                        principalTable: "station",
                        principalColumn: "code");
                });

            migrationBuilder.CreateTable(
                name: "delivery_route",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    status = table.Column<string>(type: "text", nullable: false, defaultValue: "Draft"),
                    order = table.Column<short>(type: "smallint", nullable: true),
                    length = table.Column<float>(type: "real", nullable: true),
                    start_station_id = table.Column<string>(type: "text", nullable: true),
                    snd_station_id = table.Column<string>(type: "text", nullable: true),
                    driver_code = table.Column<string>(type: "text", nullable: true),
                    expected_start_time = table.Column<int>(type: "integer", nullable: true),
                    expected_arrival_time = table.Column<int>(type: "integer", nullable: true),
                    expected_time_consumed = table.Column<int>(type: "integer", nullable: true),
                    actual_start_time = table.Column<int>(type: "integer", nullable: true),
                    actual_arrival_time = table.Column<int>(type: "integer", nullable: true),
                    actual_time_consumed = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_delivery_route", x => x.id);
                    table.ForeignKey(
                        name: "FK_delivery_route_employee_driver_code",
                        column: x => x.driver_code,
                        principalTable: "employee",
                        principalColumn: "code");
                    table.ForeignKey(
                        name: "FK_delivery_route_station_snd_station_id",
                        column: x => x.snd_station_id,
                        principalTable: "station",
                        principalColumn: "code");
                    table.ForeignKey(
                        name: "FK_delivery_route_station_start_station_id",
                        column: x => x.start_station_id,
                        principalTable: "station",
                        principalColumn: "code");
                });

            migrationBuilder.CreateTable(
                name: "delivery_route_segment",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    status = table.Column<string>(type: "text", nullable: false, defaultValue: "Draft"),
                    order = table.Column<short>(type: "smallint", nullable: true),
                    length = table.Column<float>(type: "real", nullable: true),
                    delivery_route_id = table.Column<int>(type: "integer", nullable: false),
                    start_station_id = table.Column<string>(type: "text", nullable: true),
                    snd_station_id = table.Column<string>(type: "text", nullable: true),
                    driver_code = table.Column<string>(type: "text", nullable: true),
                    expected_start_time = table.Column<int>(type: "integer", nullable: true),
                    expected_arrival_time = table.Column<int>(type: "integer", nullable: true),
                    expected_time_consumed = table.Column<int>(type: "integer", nullable: true),
                    actual_start_time = table.Column<int>(type: "integer", nullable: true),
                    actual_arrival_time = table.Column<int>(type: "integer", nullable: true),
                    actual_time_consumed = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_delivery_route_segment", x => x.id);
                    table.ForeignKey(
                        name: "FK_delivery_route_segment_delivery_route_delivery_route_id",
                        column: x => x.delivery_route_id,
                        principalTable: "delivery_route",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_delivery_route_segment_employee_driver_code",
                        column: x => x.driver_code,
                        principalTable: "employee",
                        principalColumn: "code");
                    table.ForeignKey(
                        name: "FK_delivery_route_segment_station_snd_station_id",
                        column: x => x.snd_station_id,
                        principalTable: "station",
                        principalColumn: "code");
                    table.ForeignKey(
                        name: "FK_delivery_route_segment_station_start_station_id",
                        column: x => x.start_station_id,
                        principalTable: "station",
                        principalColumn: "code");
                });

            migrationBuilder.CreateTable(
                name: "delivery_order",
                columns: table => new
                {
                    code = table.Column<string>(type: "text", nullable: false),
                    is_to_customer = table.Column<bool>(type: "boolean", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<string>(type: "text", nullable: false, defaultValue: "Draft"),
                    parent_code = table.Column<string>(type: "text", nullable: true),
                    driver_code = table.Column<string>(type: "text", nullable: true),
                    coordinator_code = table.Column<string>(type: "text", nullable: true),
                    group_code = table.Column<string>(type: "text", nullable: true),
                    session_code = table.Column<string>(type: "text", nullable: true),
                    delivery_route_segment_id = table.Column<int>(type: "integer", nullable: true),
                    source_by = table.Column<string>(type: "text", nullable: true, defaultValue: "TMS"),
                    expected_start_time = table.Column<long>(type: "bigint", nullable: true),
                    expected_arrival_time = table.Column<long>(type: "bigint", nullable: true),
                    expected_time_consumed = table.Column<long>(type: "bigint", nullable: true),
                    actual_start_time = table.Column<long>(type: "bigint", nullable: true),
                    actual_arrival_time = table.Column<long>(type: "bigint", nullable: true),
                    actual_time_consumed = table.Column<long>(type: "bigint", nullable: true),
                    reference_code = table.Column<string>(type: "text", nullable: true),
                    threepl_team = table.Column<string>(type: "text", nullable: true),
                    product_type = table.Column<string>(type: "text", nullable: true),
                    amount = table.Column<float>(type: "real", nullable: true),
                    weight = table.Column<int>(type: "integer", nullable: true),
                    cod_allowed = table.Column<bool>(type: "boolean", nullable: true),
                    cod_amount = table.Column<float>(type: "real", nullable: true),
                    cod_method = table.Column<string>(type: "text", nullable: true),
                    start_address = table.Column<string>(type: "text", nullable: true),
                    start_contact_person = table.Column<string>(type: "text", nullable: true),
                    start_contact_phone = table.Column<string>(type: "text", nullable: true),
                    start_note = table.Column<string>(type: "text", nullable: true),
                    end_address = table.Column<string>(type: "text", nullable: true),
                    end_contact_person = table.Column<string>(type: "text", nullable: true),
                    end_contact_phone = table.Column<string>(type: "text", nullable: true),
                    end_note = table.Column<string>(type: "text", nullable: true),
                    additional = table.Column<DataAttribute[]>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_delivery_order", x => x.code);
                    table.ForeignKey(
                        name: "FK_delivery_order_delivery_order_parent_code",
                        column: x => x.parent_code,
                        principalTable: "delivery_order",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_delivery_order_delivery_route_segment_delivery_route_segmen~",
                        column: x => x.delivery_route_segment_id,
                        principalTable: "delivery_route_segment",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_delivery_order_employee_coordinator_code",
                        column: x => x.coordinator_code,
                        principalTable: "employee",
                        principalColumn: "code");
                    table.ForeignKey(
                        name: "FK_delivery_order_employee_driver_code",
                        column: x => x.driver_code,
                        principalTable: "employee",
                        principalColumn: "code");
                });

            migrationBuilder.CreateTable(
                name: "delivery_package_group",
                columns: table => new
                {
                    code = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false, defaultValue: "Draft"),
                    parent_code = table.Column<string>(type: "text", nullable: true),
                    delivery_order_code = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_delivery_package_group", x => x.code);
                    table.ForeignKey(
                        name: "FK_delivery_package_group_delivery_order_delivery_order_code",
                        column: x => x.delivery_order_code,
                        principalTable: "delivery_order",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_delivery_package_group_delivery_package_group_parent_code",
                        column: x => x.parent_code,
                        principalTable: "delivery_package_group",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "delivery_package",
                columns: table => new
                {
                    code = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false, defaultValue: "Draft"),
                    delivery_package_group_code = table.Column<string>(type: "text", nullable: true),
                    external_code = table.Column<string>(type: "text", nullable: true),
                    name = table.Column<string>(type: "text", nullable: true),
                    uom = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_delivery_package", x => x.code);
                    table.ForeignKey(
                        name: "FK_delivery_package_delivery_package_group_delivery_package_gr~",
                        column: x => x.delivery_package_group_code,
                        principalTable: "delivery_package_group",
                        principalColumn: "code");
                });

            migrationBuilder.CreateTable(
                name: "delivery_order_line",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    delivery_order_code = table.Column<string>(type: "text", nullable: false),
                    delivery_package_code = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false, defaultValue: "Draft"),
                    quantity = table.Column<int>(type: "integer", nullable: true),
                    weight = table.Column<int>(type: "integer", nullable: true),
                    length = table.Column<int>(type: "integer", nullable: true),
                    width = table.Column<int>(type: "integer", nullable: true),
                    height = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_delivery_order_line", x => x.id);
                    table.ForeignKey(
                        name: "FK_delivery_order_line_delivery_order_delivery_order_code",
                        column: x => x.delivery_order_code,
                        principalTable: "delivery_order",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_delivery_order_line_delivery_package_delivery_package_code",
                        column: x => x.delivery_package_code,
                        principalTable: "delivery_package",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_delivery_order_coordinator_code",
                table: "delivery_order",
                column: "coordinator_code");

            migrationBuilder.CreateIndex(
                name: "IX_delivery_order_delivery_route_segment_id",
                table: "delivery_order",
                column: "delivery_route_segment_id");

            migrationBuilder.CreateIndex(
                name: "IX_delivery_order_driver_code",
                table: "delivery_order",
                column: "driver_code");

            migrationBuilder.CreateIndex(
                name: "IX_delivery_order_parent_code",
                table: "delivery_order",
                column: "parent_code");

            migrationBuilder.CreateIndex(
                name: "IX_delivery_order_line_delivery_order_code",
                table: "delivery_order_line",
                column: "delivery_order_code");

            migrationBuilder.CreateIndex(
                name: "IX_delivery_order_line_delivery_package_code",
                table: "delivery_order_line",
                column: "delivery_package_code");

            migrationBuilder.CreateIndex(
                name: "IX_delivery_package_delivery_package_group_code",
                table: "delivery_package",
                column: "delivery_package_group_code");

            migrationBuilder.CreateIndex(
                name: "IX_delivery_package_group_delivery_order_code",
                table: "delivery_package_group",
                column: "delivery_order_code");

            migrationBuilder.CreateIndex(
                name: "IX_delivery_package_group_parent_code",
                table: "delivery_package_group",
                column: "parent_code");

            migrationBuilder.CreateIndex(
                name: "IX_delivery_route_driver_code",
                table: "delivery_route",
                column: "driver_code");

            migrationBuilder.CreateIndex(
                name: "IX_delivery_route_snd_station_id",
                table: "delivery_route",
                column: "snd_station_id");

            migrationBuilder.CreateIndex(
                name: "IX_delivery_route_start_station_id",
                table: "delivery_route",
                column: "start_station_id");

            migrationBuilder.CreateIndex(
                name: "IX_delivery_route_segment_delivery_route_id",
                table: "delivery_route_segment",
                column: "delivery_route_id");

            migrationBuilder.CreateIndex(
                name: "IX_delivery_route_segment_driver_code",
                table: "delivery_route_segment",
                column: "driver_code");

            migrationBuilder.CreateIndex(
                name: "IX_delivery_route_segment_snd_station_id",
                table: "delivery_route_segment",
                column: "snd_station_id");

            migrationBuilder.CreateIndex(
                name: "IX_delivery_route_segment_start_station_id",
                table: "delivery_route_segment",
                column: "start_station_id");

            migrationBuilder.CreateIndex(
                name: "IX_employee_station_code",
                table: "employee",
                column: "station_code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "data_attribute");

            migrationBuilder.DropTable(
                name: "delivery_order_line");

            migrationBuilder.DropTable(
                name: "delivery_package");

            migrationBuilder.DropTable(
                name: "delivery_package_group");

            migrationBuilder.DropTable(
                name: "delivery_order");

            migrationBuilder.DropTable(
                name: "delivery_route_segment");

            migrationBuilder.DropTable(
                name: "delivery_route");

            migrationBuilder.DropTable(
                name: "employee");

            migrationBuilder.DropTable(
                name: "station");
        }
    }
}
