using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BMS.Sql.Library.Migrations
{
    public partial class Updated_ApplicationUser_Fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ApplicationUsers",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "ApplicationUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "ApplicationUsers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "ApplicationUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "ApplicationUsers");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "ApplicationUsers",
                newName: "Name");
        }
    }
}
