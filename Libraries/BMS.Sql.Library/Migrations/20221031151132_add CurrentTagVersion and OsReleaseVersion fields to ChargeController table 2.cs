using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BMS.Sql.Library.Migrations
{
    public partial class addCurrentTagVersionandOsReleaseVersionfieldstoChargeControllertable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CurrentTagVersion",
                table: "ChargeControllers",
                newName: "WebAppVersion");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WebAppVersion",
                table: "ChargeControllers",
                newName: "CurrentTagVersion");
        }
    }
}
