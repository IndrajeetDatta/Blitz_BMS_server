using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BMS.Sql.Library.Migrations
{
    public partial class AddChargeControllerChargePointRFID_RemoveChargeStation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Networks_ChargingStations_ChargingStationId",
                table: "Networks");

            migrationBuilder.DropTable(
                name: "ChargingStations");

            migrationBuilder.CreateTable(
                name: "ChargeControllers",
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
                    UptimeInSeconds = table.Column<long>(type: "bigint", nullable: false),
                    SerialNumber = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    LocationData = table.Column<string>(type: "text", nullable: false),
                    DateInstalled = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastMaintenance = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    SystemTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
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
                    ModemSimStatus = table.Column<string>(type: "text", nullable: false),
                    CPUTemperatureCelsius = table.Column<int>(type: "integer", nullable: false),
                    CPUUtilizationPercentage = table.Column<int>(type: "integer", nullable: false),
                    RAMAvailableBytes = table.Column<long>(type: "bigint", nullable: false),
                    RAMTotalBytes = table.Column<long>(type: "bigint", nullable: false),
                    RAMUsedBytes = table.Column<long>(type: "bigint", nullable: false),
                    DataUtilizationPercentage = table.Column<int>(type: "integer", nullable: false),
                    DataTotalBytes = table.Column<long>(type: "bigint", nullable: false),
                    LogUtilizationPercentage = table.Column<int>(type: "integer", nullable: false),
                    LogTotalBytes = table.Column<long>(type: "bigint", nullable: false),
                    VarVolatileUtilizationPercentage = table.Column<int>(type: "integer", nullable: false),
                    VarVolatileTotalBytes = table.Column<long>(type: "bigint", nullable: false),
                    SystemMonitorVersion = table.Column<string>(type: "text", nullable: false),
                    SystemMonitorStatus = table.Column<string>(type: "text", nullable: false),
                    ControllerAgentVersion = table.Column<string>(type: "text", nullable: false),
                    ControllerAgent_Status = table.Column<string>(type: "text", nullable: false),
                    OCPP16_Version = table.Column<string>(type: "text", nullable: false),
                    OCPP16Status = table.Column<string>(type: "text", nullable: false),
                    ModbusClientVersion = table.Column<string>(type: "text", nullable: false),
                    ModbusClientStatus = table.Column<string>(type: "text", nullable: false),
                    ModbusServerVersion = table.Column<string>(type: "text", nullable: false),
                    ModbusServerStatus = table.Column<string>(type: "text", nullable: false),
                    JupicoreVersion = table.Column<string>(type: "text", nullable: false),
                    JupicoreStatus = table.Column<string>(type: "text", nullable: false),
                    LoadManagementVersion = table.Column<string>(type: "text", nullable: false),
                    LoadManagementStatus = table.Column<string>(type: "text", nullable: false),
                    WebserverVersion = table.Column<string>(type: "text", nullable: false),
                    WebserverStatus = table.Column<string>(type: "text", nullable: false),
                    LoadManagementActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChargeControllers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChargeControllers_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChargeControllers_Installers_InstallerId",
                        column: x => x.InstallerId,
                        principalTable: "Installers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChargePoints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ChargeControllerId = table.Column<int>(type: "integer", nullable: false),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false),
                    ChargingTimeInSeconds = table.Column<int>(type: "integer", nullable: false),
                    ChargeCurrentMinimumInAmpers = table.Column<int>(type: "integer", nullable: false),
                    ChargeCurrentMaximumInAmpers = table.Column<int>(type: "integer", nullable: false),
                    FallbackCurrentInAmpers = table.Column<int>(type: "integer", nullable: false),
                    FallbackTimeInSeconds = table.Column<int>(type: "integer", nullable: false),
                    RFIDTimeoutInSeconds = table.Column<int>(type: "integer", nullable: false),
                    OCPPConnectorId = table.Column<int>(type: "integer", nullable: false),
                    ChargingRate = table.Column<decimal>(type: "numeric", nullable: false),
                    SerialNumber = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    SoftwareVersion = table.Column<string>(type: "text", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false),
                    Heartbeat = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    State = table.Column<string>(type: "text", nullable: false),
                    Configured = table.Column<string>(type: "text", nullable: false),
                    EnergyType = table.Column<string>(type: "text", nullable: false),
                    PhaseRotation = table.Column<string>(type: "text", nullable: false),
                    ChargingMode = table.Column<string>(type: "text", nullable: false),
                    RFIDReader = table.Column<string>(type: "text", nullable: false),
                    RFIDReaderType = table.Column<string>(type: "text", nullable: false),
                    HighLevelCommunication = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChargePoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChargePoints_ChargeControllers_ChargeControllerId",
                        column: x => x.ChargeControllerId,
                        principalTable: "ChargeControllers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RFID",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ChargeControllerId = table.Column<int>(type: "integer", nullable: false),
                    AllowCharging = table.Column<bool>(type: "boolean", nullable: false),
                    EvConsumptionRateKWhPer100KM = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    SerialNumber = table.Column<string>(type: "text", nullable: false),
                    ExpiryDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RFID", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RFID_ChargeControllers_ChargeControllerId",
                        column: x => x.ChargeControllerId,
                        principalTable: "ChargeControllers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChargeControllers_ClientId",
                table: "ChargeControllers",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ChargeControllers_InstallerId",
                table: "ChargeControllers",
                column: "InstallerId");

            migrationBuilder.CreateIndex(
                name: "IX_ChargePoints_ChargeControllerId",
                table: "ChargePoints",
                column: "ChargeControllerId");

            migrationBuilder.CreateIndex(
                name: "IX_RFID_ChargeControllerId",
                table: "RFID",
                column: "ChargeControllerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Networks_ChargeControllers_ChargingStationId",
                table: "Networks",
                column: "ChargingStationId",
                principalTable: "ChargeControllers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Networks_ChargeControllers_ChargingStationId",
                table: "Networks");

            migrationBuilder.DropTable(
                name: "ChargePoints");

            migrationBuilder.DropTable(
                name: "RFID");

            migrationBuilder.DropTable(
                name: "ChargeControllers");

            migrationBuilder.CreateTable(
                name: "ChargingStations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientId = table.Column<int>(type: "integer", nullable: false),
                    InstallerId = table.Column<int>(type: "integer", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    Configured = table.Column<string>(type: "text", nullable: false),
                    DateInstalled = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ETH0DHCP = table.Column<bool>(type: "boolean", nullable: false),
                    ETH0Gateway = table.Column<string>(type: "text", nullable: false),
                    ETH0IPAddress = table.Column<string>(type: "text", nullable: false),
                    ETH0SubnetMask = table.Column<string>(type: "text", nullable: false),
                    Heartbeat = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastMaintenance = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LoadManagement = table.Column<bool>(type: "boolean", nullable: false),
                    LocationData = table.Column<string>(type: "text", nullable: false),
                    ModemAPN = table.Column<string>(type: "text", nullable: false),
                    ModemConnectionStatus = table.Column<string>(type: "text", nullable: false),
                    ModemDefaultRoute = table.Column<bool>(type: "boolean", nullable: false),
                    ModemExtendedReport = table.Column<string>(type: "text", nullable: false),
                    ModemICCID = table.Column<string>(type: "text", nullable: false),
                    ModemIMSI = table.Column<string>(type: "text", nullable: false),
                    ModemIPAddress = table.Column<string>(type: "text", nullable: false),
                    ModemPassword = table.Column<string>(type: "text", nullable: false),
                    ModemPreferOverETH0 = table.Column<bool>(type: "boolean", nullable: false),
                    ModemPrimaryDNSServer = table.Column<string>(type: "text", nullable: false),
                    ModemProvider = table.Column<string>(type: "text", nullable: false),
                    ModemRadioTechnology = table.Column<string>(type: "text", nullable: false),
                    ModemReceivedBytes = table.Column<long>(type: "bigint", nullable: false),
                    ModemRegistrationStatus = table.Column<string>(type: "text", nullable: false),
                    ModemRoamingStatus = table.Column<string>(type: "text", nullable: false),
                    ModemSecondaryDNSServer = table.Column<string>(type: "text", nullable: false),
                    ModemServiceActive = table.Column<bool>(type: "boolean", nullable: false),
                    ModemSignalCQI = table.Column<int>(type: "integer", nullable: false),
                    ModemSignalQuality = table.Column<string>(type: "text", nullable: false),
                    ModemSignalRSSI = table.Column<int>(type: "integer", nullable: false),
                    ModemSimPin = table.Column<string>(type: "text", nullable: false),
                    ModemSimStatus = table.Column<string>(type: "text", nullable: false),
                    ModemTransmittedBytes = table.Column<long>(type: "bigint", nullable: false),
                    ModemUsername = table.Column<string>(type: "text", nullable: false),
                    NetworkConnection = table.Column<bool>(type: "boolean", nullable: false),
                    NetworkType = table.Column<string>(type: "text", nullable: false),
                    OCPPConnection = table.Column<bool>(type: "boolean", nullable: false),
                    SerialNumber = table.Column<string>(type: "text", nullable: false),
                    SimActive = table.Column<bool>(type: "boolean", nullable: false),
                    SoftwareVersion = table.Column<string>(type: "text", nullable: false),
                    SystemTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Uptime = table.Column<int>(type: "integer", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_ChargingStations_ClientId",
                table: "ChargingStations",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ChargingStations_InstallerId",
                table: "ChargingStations",
                column: "InstallerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Networks_ChargingStations_ChargingStationId",
                table: "Networks",
                column: "ChargingStationId",
                principalTable: "ChargingStations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
