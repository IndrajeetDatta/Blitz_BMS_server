using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BMS.Sql.Library.Migrations
{
    public partial class addCurrentTagVersionandOsReleaseVersionfieldstoChargeControllertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CurrentTagVersion",
                table: "ChargeControllers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OsReleaseVersion",
                table: "ChargeControllers",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentTagVersion",
                table: "ChargeControllers");

            migrationBuilder.DropColumn(
                name: "OsReleaseVersion",
                table: "ChargeControllers");
        }
    }
}
