using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BMS.Sql.Library.Migrations
{
    public partial class remove_Installers_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChargeControllers_Installers_InstallerId",
                table: "ChargeControllers");

            migrationBuilder.DropTable(
                name: "Installers");

            migrationBuilder.AddForeignKey(
                name: "FK_ChargeControllers_ApplicationUsers_InstallerId",
                table: "ChargeControllers",
                column: "InstallerId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChargeControllers_ApplicationUsers_InstallerId",
                table: "ChargeControllers");

            migrationBuilder.CreateTable(
                name: "Installers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Installers", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ChargeControllers_Installers_InstallerId",
                table: "ChargeControllers",
                column: "InstallerId",
                principalTable: "Installers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
