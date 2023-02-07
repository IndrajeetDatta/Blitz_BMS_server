using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BMS.Sql.Library.Migrations
{
    public partial class addFromLastHeartbeatfieldtoOcppMEssagetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "status",
                table: "OCPPStatus",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "operative",
                table: "OCPPStatus",
                newName: "Operative");

            migrationBuilder.RenameColumn(
                name: "occpStatusSentDate",
                table: "OCPPStatus",
                newName: "OccpStatusSentDate");

            migrationBuilder.RenameColumn(
                name: "occpStatus",
                table: "OCPPStatus",
                newName: "OccpStatus");

            migrationBuilder.RenameColumn(
                name: "device_uid",
                table: "OCPPStatus",
                newName: "Device_uid");

            migrationBuilder.RenameColumn(
                name: "type",
                table: "OCPPMessage",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "timestamp",
                table: "OCPPMessage",
                newName: "Timestamp");

            migrationBuilder.RenameColumn(
                name: "messageData",
                table: "OCPPMessage",
                newName: "MessageData");

            migrationBuilder.RenameColumn(
                name: "action",
                table: "OCPPMessage",
                newName: "Action");

            migrationBuilder.RenameColumn(
                name: "serviceRFID",
                table: "OCPPConfig",
                newName: "ServiceRFID");

            migrationBuilder.RenameColumn(
                name: "ocppProtocolVersion",
                table: "OCPPConfig",
                newName: "OcppProtocolVersion");

            migrationBuilder.RenameColumn(
                name: "networkInterface",
                table: "OCPPConfig",
                newName: "NetworkInterface");

            migrationBuilder.RenameColumn(
                name: "freeModeRFID",
                table: "OCPPConfig",
                newName: "FreeModeRFID");

            migrationBuilder.RenameColumn(
                name: "chargeStationVendor",
                table: "OCPPConfig",
                newName: "ChargeStationVendor");

            migrationBuilder.RenameColumn(
                name: "chargeStationSerialNumber",
                table: "OCPPConfig",
                newName: "ChargeStationSerialNumber");

            migrationBuilder.RenameColumn(
                name: "chargeStationModel",
                table: "OCPPConfig",
                newName: "ChargeStationModel");

            migrationBuilder.RenameColumn(
                name: "backendURL",
                table: "OCPPConfig",
                newName: "BackendURL");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Log",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "jsonData",
                table: "Log",
                newName: "JsonData");

            migrationBuilder.RenameColumn(
                name: "ipRequest",
                table: "Log",
                newName: "IpRequest");

            migrationBuilder.RenameColumn(
                name: "errorMessage",
                table: "Log",
                newName: "ErrorMessage");

            migrationBuilder.RenameColumn(
                name: "created",
                table: "Log",
                newName: "CreatedDate");

            migrationBuilder.AddColumn<bool>(
                name: "FromLastHeartbeat",
                table: "OCPPMessage",
                type: "boolean",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromLastHeartbeat",
                table: "OCPPMessage");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "OCPPStatus",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Operative",
                table: "OCPPStatus",
                newName: "operative");

            migrationBuilder.RenameColumn(
                name: "OccpStatusSentDate",
                table: "OCPPStatus",
                newName: "occpStatusSentDate");

            migrationBuilder.RenameColumn(
                name: "OccpStatus",
                table: "OCPPStatus",
                newName: "occpStatus");

            migrationBuilder.RenameColumn(
                name: "Device_uid",
                table: "OCPPStatus",
                newName: "device_uid");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "OCPPMessage",
                newName: "type");

            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "OCPPMessage",
                newName: "timestamp");

            migrationBuilder.RenameColumn(
                name: "MessageData",
                table: "OCPPMessage",
                newName: "messageData");

            migrationBuilder.RenameColumn(
                name: "Action",
                table: "OCPPMessage",
                newName: "action");

            migrationBuilder.RenameColumn(
                name: "ServiceRFID",
                table: "OCPPConfig",
                newName: "serviceRFID");

            migrationBuilder.RenameColumn(
                name: "OcppProtocolVersion",
                table: "OCPPConfig",
                newName: "ocppProtocolVersion");

            migrationBuilder.RenameColumn(
                name: "NetworkInterface",
                table: "OCPPConfig",
                newName: "networkInterface");

            migrationBuilder.RenameColumn(
                name: "FreeModeRFID",
                table: "OCPPConfig",
                newName: "freeModeRFID");

            migrationBuilder.RenameColumn(
                name: "ChargeStationVendor",
                table: "OCPPConfig",
                newName: "chargeStationVendor");

            migrationBuilder.RenameColumn(
                name: "ChargeStationSerialNumber",
                table: "OCPPConfig",
                newName: "chargeStationSerialNumber");

            migrationBuilder.RenameColumn(
                name: "ChargeStationModel",
                table: "OCPPConfig",
                newName: "chargeStationModel");

            migrationBuilder.RenameColumn(
                name: "BackendURL",
                table: "OCPPConfig",
                newName: "backendURL");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Log",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "JsonData",
                table: "Log",
                newName: "jsonData");

            migrationBuilder.RenameColumn(
                name: "IpRequest",
                table: "Log",
                newName: "ipRequest");

            migrationBuilder.RenameColumn(
                name: "ErrorMessage",
                table: "Log",
                newName: "errorMessage");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Log",
                newName: "created");
        }
    }
}
