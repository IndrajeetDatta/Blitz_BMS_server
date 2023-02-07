using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BMS.Sql.Library.Migrations
{
    public partial class makeCammelCasetofieldsfromCommandtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "status",
                table: "Command",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "processedDate",
                table: "Command",
                newName: "ProcessedDate");

            migrationBuilder.RenameColumn(
                name: "masterId",
                table: "Command",
                newName: "MasterId");

            migrationBuilder.RenameColumn(
                name: "createdDate",
                table: "Command",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "chargePointUid",
                table: "Command",
                newName: "ChargePointUid");

            migrationBuilder.RenameColumn(
                name: "chargeControllerUid",
                table: "Command",
                newName: "ChargeControllerUid");

            migrationBuilder.RenameColumn(
                name: "command",
                table: "Command",
                newName: "CommandType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Command",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "ProcessedDate",
                table: "Command",
                newName: "processedDate");

            migrationBuilder.RenameColumn(
                name: "MasterId",
                table: "Command",
                newName: "masterId");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Command",
                newName: "createdDate");

            migrationBuilder.RenameColumn(
                name: "ChargePointUid",
                table: "Command",
                newName: "chargePointUid");

            migrationBuilder.RenameColumn(
                name: "ChargeControllerUid",
                table: "Command",
                newName: "chargeControllerUid");

            migrationBuilder.RenameColumn(
                name: "CommandType",
                table: "Command",
                newName: "command");
        }
    }
}
