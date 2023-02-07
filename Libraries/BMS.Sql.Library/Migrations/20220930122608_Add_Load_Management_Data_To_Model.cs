using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BMS.Sql.Library.Migrations
{
    public partial class Add_Load_Management_Data_To_Model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChargingParkName",
                table: "ChargeControllers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "CurrentL1",
                table: "ChargeControllers",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "CurrentL2",
                table: "ChargeControllers",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "CurrentL3",
                table: "ChargeControllers",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HighLevelMeasuringDevice",
                table: "ChargeControllers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LoadCircuitFuse",
                table: "ChargeControllers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LoadStrategy",
                table: "ChargeControllers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MeasuringDeviceType",
                table: "ChargeControllers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "PlannedCurrentL1",
                table: "ChargeControllers",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PlannedCurrentL2",
                table: "ChargeControllers",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PlannedCurrentL3",
                table: "ChargeControllers",
                type: "double precision",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChargingParkName",
                table: "ChargeControllers");

            migrationBuilder.DropColumn(
                name: "CurrentL1",
                table: "ChargeControllers");

            migrationBuilder.DropColumn(
                name: "CurrentL2",
                table: "ChargeControllers");

            migrationBuilder.DropColumn(
                name: "CurrentL3",
                table: "ChargeControllers");

            migrationBuilder.DropColumn(
                name: "HighLevelMeasuringDevice",
                table: "ChargeControllers");

            migrationBuilder.DropColumn(
                name: "LoadCircuitFuse",
                table: "ChargeControllers");

            migrationBuilder.DropColumn(
                name: "LoadStrategy",
                table: "ChargeControllers");

            migrationBuilder.DropColumn(
                name: "MeasuringDeviceType",
                table: "ChargeControllers");

            migrationBuilder.DropColumn(
                name: "PlannedCurrentL1",
                table: "ChargeControllers");

            migrationBuilder.DropColumn(
                name: "PlannedCurrentL2",
                table: "ChargeControllers");

            migrationBuilder.DropColumn(
                name: "PlannedCurrentL3",
                table: "ChargeControllers");
        }
    }
}
