using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BMS.Sql.Library.Migrations
{
    public partial class mergeChargingModeandConfiguredintoReleaseChargingModefromChargePointtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChargingMode",
                table: "ChargePoints");

            migrationBuilder.RenameColumn(
                name: "Configured",
                table: "ChargePoints",
                newName: "ReleaseChargingMode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReleaseChargingMode",
                table: "ChargePoints",
                newName: "Configured");

            migrationBuilder.AddColumn<string>(
                name: "ChargingMode",
                table: "ChargePoints",
                type: "text",
                nullable: true);
        }
    }
}
