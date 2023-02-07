using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BMS.Sql.Library.Migrations
{
    public partial class RemoveactiveandloadmanagementcolumnfromChargingController : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "ChargeControllers");

            migrationBuilder.DropColumn(
                name: "LoadManagement",
                table: "ChargeControllers");

            migrationBuilder.AlterColumn<string>(
                name: "OCPPConnection",
                table: "ChargeControllers",
                type: "text",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "OCPPConnection",
                table: "ChargeControllers",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "ChargeControllers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LoadManagement",
                table: "ChargeControllers",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
