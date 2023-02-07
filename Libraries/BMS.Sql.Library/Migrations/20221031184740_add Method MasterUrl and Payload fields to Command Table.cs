using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BMS.Sql.Library.Migrations
{
    public partial class addMethodMasterUrlandPayloadfieldstoCommandTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MasterUrl",
                table: "Command",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Method",
                table: "Command",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Payload",
                table: "Command",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MasterUrl",
                table: "Command");

            migrationBuilder.DropColumn(
                name: "Method",
                table: "Command");

            migrationBuilder.DropColumn(
                name: "Payload",
                table: "Command");
        }
    }
}
