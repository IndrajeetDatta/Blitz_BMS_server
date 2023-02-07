using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BMS.Sql.Library.Migrations
{
    public partial class addtokenRequiredandporttoCommandtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Port",
                table: "Commands",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TokenRequired",
                table: "Commands",
                type: "boolean",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Port",
                table: "Commands");

            migrationBuilder.DropColumn(
                name: "TokenRequired",
                table: "Commands");
        }
    }
}
