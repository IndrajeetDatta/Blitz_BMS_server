using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BMS.Sql.Library.Migrations
{
    public partial class addUseAccessCredentialsfieldtoChargeControllertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "UseAccessCredentials",
                table: "ChargeControllers",
                type: "boolean",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UseAccessCredentials",
                table: "ChargeControllers");
        }
    }
}
