using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BMS.Sql.Library.Migrations
{
    public partial class create_transactions_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ChargeControllerId = table.Column<int>(type: "integer", nullable: false),
                    ChargePointId = table.Column<int>(type: "integer", nullable: false),
                    ChargePointName = table.Column<string>(type: "text", nullable: true),
                    RfidTag = table.Column<string>(type: "text", nullable: true),
                    RfidName = table.Column<string>(type: "text", nullable: true),
                    StartDay = table.Column<string>(type: "text", nullable: true),
                    StartMonth = table.Column<string>(type: "text", nullable: true),
                    StartYear = table.Column<string>(type: "text", nullable: true),
                    StartDayOfWeek = table.Column<string>(type: "text", nullable: true),
                    StartTime = table.Column<string>(type: "text", nullable: true),
                    EndTime = table.Column<string>(type: "text", nullable: true),
                    DurationDays = table.Column<string>(type: "text", nullable: true),
                    ConnectedTimeSec = table.Column<string>(type: "text", nullable: true),
                    ChargeTimeSec = table.Column<string>(type: "text", nullable: true),
                    AveragePower = table.Column<double>(type: "double precision", nullable: true),
                    ChargedEnergy = table.Column<double>(type: "double precision", nullable: true),
                    ChargedDistance = table.Column<double>(type: "double precision", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_ChargeControllers_ChargeControllerId",
                        column: x => x.ChargeControllerId,
                        principalTable: "ChargeControllers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ChargeControllerId",
                table: "Transactions",
                column: "ChargeControllerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");
        }
    }
}
