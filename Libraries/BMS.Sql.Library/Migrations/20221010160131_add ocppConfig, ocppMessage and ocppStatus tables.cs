using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BMS.Sql.Library.Migrations
{
    public partial class addocppConfigocppMessageandocppStatustables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OCPPConfig",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ChargeControllerId = table.Column<int>(type: "integer", nullable: false),
                    ocppProtocolVersion = table.Column<string>(type: "text", nullable: true),
                    networkInterface = table.Column<string>(type: "text", nullable: true),
                    backendURL = table.Column<string>(type: "text", nullable: true),
                    serviceRFID = table.Column<string>(type: "text", nullable: true),
                    freeModeRFID = table.Column<string>(type: "text", nullable: true),
                    chargeStationModel = table.Column<string>(type: "text", nullable: true),
                    chargeStationVendor = table.Column<string>(type: "text", nullable: true),
                    chargeStationSerialNumber = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OCPPConfig", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OCPPConfig_ChargeControllers_ChargeControllerId",
                        column: x => x.ChargeControllerId,
                        principalTable: "ChargeControllers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OCPPMessage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ChargeControllerId = table.Column<int>(type: "integer", nullable: false),
                    timestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    type = table.Column<int>(type: "integer", nullable: true),
                    action = table.Column<string>(type: "text", nullable: true),
                    messageData = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OCPPMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OCPPMessage_ChargeControllers_ChargeControllerId",
                        column: x => x.ChargeControllerId,
                        principalTable: "ChargeControllers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OCPPStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ChargeControllerId = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<string>(type: "text", nullable: true),
                    device_uid = table.Column<string>(type: "text", nullable: true),
                    occpStatus = table.Column<string>(type: "text", nullable: true),
                    occpStatusSentDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    operative = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OCPPStatus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OCPPStatus_ChargeControllers_ChargeControllerId",
                        column: x => x.ChargeControllerId,
                        principalTable: "ChargeControllers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OCPPConfig_ChargeControllerId",
                table: "OCPPConfig",
                column: "ChargeControllerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OCPPMessage_ChargeControllerId",
                table: "OCPPMessage",
                column: "ChargeControllerId");

            migrationBuilder.CreateIndex(
                name: "IX_OCPPStatus_ChargeControllerId",
                table: "OCPPStatus",
                column: "ChargeControllerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OCPPConfig");

            migrationBuilder.DropTable(
                name: "OCPPMessage");

            migrationBuilder.DropTable(
                name: "OCPPStatus");
        }
    }
}
