using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BMS.Sql.Library.Migrations
{
    public partial class AddChargingStationAndRelatedEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Installers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Installers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChargingStations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientId = table.Column<int>(type: "integer", nullable: false),
                    InstallerId = table.Column<int>(type: "integer", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    NetworkConnection = table.Column<bool>(type: "boolean", nullable: false),
                    LoadManagement = table.Column<bool>(type: "boolean", nullable: false),
                    OCPPConnection = table.Column<bool>(type: "boolean", nullable: false),
                    SimActive = table.Column<bool>(type: "boolean", nullable: false),
                    Uptime = table.Column<int>(type: "integer", nullable: false),
                    SerialNumber = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    LocationData = table.Column<string>(type: "text", nullable: false),
                    SoftwareVersion = table.Column<string>(type: "text", nullable: false),
                    Heartbeat = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    DateInstalled = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastMaintenance = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    SystemTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Configured = table.Column<string>(type: "text", nullable: false),
                    NetworkType = table.Column<string>(type: "text", nullable: false),
                    ETH0DHCP = table.Column<bool>(type: "boolean", nullable: false),
                    ETH0IPAddress = table.Column<string>(type: "text", nullable: false),
                    ETH0SubnetMask = table.Column<string>(type: "text", nullable: false),
                    ETH0Gateway = table.Column<string>(type: "text", nullable: false),
                    ModemServiceActive = table.Column<bool>(type: "boolean", nullable: false),
                    ModemPreferOverETH0 = table.Column<bool>(type: "boolean", nullable: false),
                    ModemDefaultRoute = table.Column<bool>(type: "boolean", nullable: false),
                    ModemSignalRSSI = table.Column<int>(type: "integer", nullable: false),
                    ModemSignalCQI = table.Column<int>(type: "integer", nullable: false),
                    ModemReceivedBytes = table.Column<long>(type: "bigint", nullable: false),
                    ModemTransmittedBytes = table.Column<long>(type: "bigint", nullable: false),
                    ModemAPN = table.Column<string>(type: "text", nullable: false),
                    ModemSimPin = table.Column<string>(type: "text", nullable: false),
                    ModemUsername = table.Column<string>(type: "text", nullable: false),
                    ModemPassword = table.Column<string>(type: "text", nullable: false),
                    ModemIPAddress = table.Column<string>(type: "text", nullable: false),
                    ModemPrimaryDNSServer = table.Column<string>(type: "text", nullable: false),
                    ModemSecondaryDNSServer = table.Column<string>(type: "text", nullable: false),
                    ModemIMSI = table.Column<string>(type: "text", nullable: false),
                    ModemICCID = table.Column<string>(type: "text", nullable: false),
                    ModemExtendedReport = table.Column<string>(type: "text", nullable: false),
                    ModemConnectionStatus = table.Column<string>(type: "text", nullable: false),
                    ModemProvider = table.Column<string>(type: "text", nullable: false),
                    ModemRegistrationStatus = table.Column<string>(type: "text", nullable: false),
                    ModemRoamingStatus = table.Column<string>(type: "text", nullable: false),
                    ModemSignalQuality = table.Column<string>(type: "text", nullable: false),
                    ModemRadioTechnology = table.Column<string>(type: "text", nullable: false),
                    ModemSimStatus = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChargingStations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChargingStations_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChargingStations_Installers_InstallerId",
                        column: x => x.InstallerId,
                        principalTable: "Installers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Networks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ChargingStationId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    IPV4Address = table.Column<string>(type: "text", nullable: false),
                    IPV6Address = table.Column<string>(type: "text", nullable: false),
                    MACAddress = table.Column<string>(type: "text", nullable: false),
                    ReceivedBytes = table.Column<long>(type: "bigint", nullable: false),
                    TransmittedBytes = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Networks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Networks_ChargingStations_ChargingStationId",
                        column: x => x.ChargingStationId,
                        principalTable: "ChargingStations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChargingStations_ClientId",
                table: "ChargingStations",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ChargingStations_InstallerId",
                table: "ChargingStations",
                column: "InstallerId");

            migrationBuilder.CreateIndex(
                name: "IX_Networks_ChargingStationId",
                table: "Networks",
                column: "ChargingStationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Networks");

            migrationBuilder.DropTable(
                name: "ChargingStations");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Installers");
        }
    }
}
