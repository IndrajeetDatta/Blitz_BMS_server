using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BMS.Sql.Library.Migrations
{
    public partial class addpartialheartbeattoChargeControllertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PayloadType",
                table: "ChargeControllers");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "PartialHeartbeat",
                table: "ChargeControllers",
                type: "timestamp with time zone",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PartialHeartbeat",
                table: "ChargeControllers");

            migrationBuilder.AddColumn<string>(
                name: "PayloadType",
                table: "ChargeControllers",
                type: "text",
                nullable: true);
        }
    }
}
