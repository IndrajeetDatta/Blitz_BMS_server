using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BMS.Sql.Library.Migrations
{
    public partial class addemunforCommandStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Log",
                table: "Log");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Command",
                table: "Command");

            migrationBuilder.RenameTable(
                name: "Log",
                newName: "Logs");

            migrationBuilder.RenameTable(
                name: "Command",
                newName: "Commands");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Logs",
                table: "Logs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Commands",
                table: "Commands",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Logs",
                table: "Logs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Commands",
                table: "Commands");

            migrationBuilder.RenameTable(
                name: "Logs",
                newName: "Log");

            migrationBuilder.RenameTable(
                name: "Commands",
                newName: "Command");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Log",
                table: "Log",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Command",
                table: "Command",
                column: "Id");
        }
    }
}
