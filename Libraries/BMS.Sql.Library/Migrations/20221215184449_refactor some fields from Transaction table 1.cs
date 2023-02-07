using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BMS.Sql.Library.Migrations
{
    public partial class refactorsomefieldsfromTransactiontable1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChargeTimeSec",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ConnectedTimeSec",
                table: "Transactions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChargeTimeSec",
                table: "Transactions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConnectedTimeSec",
                table: "Transactions",
                type: "text",
                nullable: true);
        }
    }
}
