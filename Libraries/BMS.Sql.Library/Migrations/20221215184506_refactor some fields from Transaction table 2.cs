using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BMS.Sql.Library.Migrations
{
    public partial class refactorsomefieldsfromTransactiontable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ChargeTimeSec",
                table: "Transactions",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ConnectedTimeSec",
                table: "Transactions",
                type: "double precision",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChargeTimeSec",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ConnectedTimeSec",
                table: "Transactions");
        }
    }
}
