using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BMS.Sql.Library.Migrations
{
    public partial class addchargePointUidandchargeControllerUidonChargePoint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Uid",
                table: "ChargePoints",
                newName: "ChargePointUid");

            migrationBuilder.AddColumn<string>(
                name: "ChargeControllerUid",
                table: "ChargePoints",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChargeControllerUid",
                table: "ChargePoints");

            migrationBuilder.RenameColumn(
                name: "ChargePointUid",
                table: "ChargePoints",
                newName: "Uid");
        }
    }
}
