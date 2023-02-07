using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BMS.Sql.Library.Migrations
{
    public partial class addRfidSerialNumbertoCommandtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RfidSerialNumber",
                table: "Commands",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RfidSerialNumber",
                table: "Commands");
        }
    }
}
