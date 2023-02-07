using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BMS.Sql.Library.Migrations
{
    public partial class addSupervisionMeterCurrentfieldstoChargeControllertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PlannedCurrentL3",
                table: "ChargeControllers",
                newName: "SupervisionMeterCurrentI3");

            migrationBuilder.RenameColumn(
                name: "PlannedCurrentL2",
                table: "ChargeControllers",
                newName: "SupervisionMeterCurrentI2");

            migrationBuilder.RenameColumn(
                name: "PlannedCurrentL1",
                table: "ChargeControllers",
                newName: "SupervisionMeterCurrentI1");

            migrationBuilder.RenameColumn(
                name: "CurrentL3",
                table: "ChargeControllers",
                newName: "PlannedCurrentI3");

            migrationBuilder.RenameColumn(
                name: "CurrentL2",
                table: "ChargeControllers",
                newName: "PlannedCurrentI2");

            migrationBuilder.RenameColumn(
                name: "CurrentL1",
                table: "ChargeControllers",
                newName: "PlannedCurrentI1");

            migrationBuilder.AddColumn<string>(
                name: "CurrentI1",
                table: "ChargeControllers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrentI2",
                table: "ChargeControllers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrentI3",
                table: "ChargeControllers",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentI1",
                table: "ChargeControllers");

            migrationBuilder.DropColumn(
                name: "CurrentI2",
                table: "ChargeControllers");

            migrationBuilder.DropColumn(
                name: "CurrentI3",
                table: "ChargeControllers");

            migrationBuilder.RenameColumn(
                name: "SupervisionMeterCurrentI3",
                table: "ChargeControllers",
                newName: "PlannedCurrentL3");

            migrationBuilder.RenameColumn(
                name: "SupervisionMeterCurrentI2",
                table: "ChargeControllers",
                newName: "PlannedCurrentL2");

            migrationBuilder.RenameColumn(
                name: "SupervisionMeterCurrentI1",
                table: "ChargeControllers",
                newName: "PlannedCurrentL1");

            migrationBuilder.RenameColumn(
                name: "PlannedCurrentI3",
                table: "ChargeControllers",
                newName: "CurrentL3");

            migrationBuilder.RenameColumn(
                name: "PlannedCurrentI2",
                table: "ChargeControllers",
                newName: "CurrentL2");

            migrationBuilder.RenameColumn(
                name: "PlannedCurrentI1",
                table: "ChargeControllers",
                newName: "CurrentL1");
        }
    }
}
