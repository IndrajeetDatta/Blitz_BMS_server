using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BMS.Sql.Library.Migrations
{
    public partial class addfullheartbeattoChargeControllertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PartialHeartbeat",
                table: "ChargeControllers",
                newName: "FullHeartbeat");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullHeartbeat",
                table: "ChargeControllers",
                newName: "PartialHeartbeat");
        }
    }
}
