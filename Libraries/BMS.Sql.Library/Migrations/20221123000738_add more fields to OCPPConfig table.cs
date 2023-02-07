using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BMS.Sql.Library.Migrations
{
    public partial class addmorefieldstoOCPPConfigtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChargeBoxID",
                table: "OCPPConfig",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChargeBoxSerialNumber",
                table: "OCPPConfig",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "FreeMode",
                table: "OCPPConfig",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Iccid",
                table: "OCPPConfig",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Imsi",
                table: "OCPPConfig",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MeterSerialNumber",
                table: "OCPPConfig",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MeterType",
                table: "OCPPConfig",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ServiceRestart",
                table: "OCPPConfig",
                type: "boolean",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChargeBoxID",
                table: "OCPPConfig");

            migrationBuilder.DropColumn(
                name: "ChargeBoxSerialNumber",
                table: "OCPPConfig");

            migrationBuilder.DropColumn(
                name: "FreeMode",
                table: "OCPPConfig");

            migrationBuilder.DropColumn(
                name: "Iccid",
                table: "OCPPConfig");

            migrationBuilder.DropColumn(
                name: "Imsi",
                table: "OCPPConfig");

            migrationBuilder.DropColumn(
                name: "MeterSerialNumber",
                table: "OCPPConfig");

            migrationBuilder.DropColumn(
                name: "MeterType",
                table: "OCPPConfig");

            migrationBuilder.DropColumn(
                name: "ServiceRestart",
                table: "OCPPConfig");
        }
    }
}
