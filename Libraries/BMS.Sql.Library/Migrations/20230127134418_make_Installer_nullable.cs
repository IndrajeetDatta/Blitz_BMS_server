using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BMS.Sql.Library.Migrations
{
    public partial class make_Installer_nullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChargeControllers_ApplicationUsers_InstallerId",
                table: "ChargeControllers");

            migrationBuilder.AlterColumn<int>(
                name: "InstallerId",
                table: "ChargeControllers",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_ChargeControllers_ApplicationUsers_InstallerId",
                table: "ChargeControllers",
                column: "InstallerId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChargeControllers_ApplicationUsers_InstallerId",
                table: "ChargeControllers");

            migrationBuilder.AlterColumn<int>(
                name: "InstallerId",
                table: "ChargeControllers",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ChargeControllers_ApplicationUsers_InstallerId",
                table: "ChargeControllers",
                column: "InstallerId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
