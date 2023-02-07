using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BMS.Sql.Library.Migrations
{
    public partial class Brought_back_the_serial_number_for_charge_points : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SerialNumber",
                table: "ChargePoints",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SerialNumber",
                table: "ChargePoints");
        }
    }
}
