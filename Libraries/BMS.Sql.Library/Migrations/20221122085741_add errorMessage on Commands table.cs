using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BMS.Sql.Library.Migrations
{
    public partial class adderrorMessageonCommandstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ErrorMessage",
                table: "Commands",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ErrorMessage",
                table: "Commands");
        }
    }
}
