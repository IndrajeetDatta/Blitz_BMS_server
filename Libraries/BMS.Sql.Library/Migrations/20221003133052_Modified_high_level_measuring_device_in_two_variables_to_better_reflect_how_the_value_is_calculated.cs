using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BMS.Sql.Library.Migrations
{
    public partial class Modified_high_level_measuring_device_in_two_variables_to_better_reflect_how_the_value_is_calculated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HighLevelMeasuringDevice",
                table: "ChargeControllers");

            migrationBuilder.AlterColumn<string>(
                name: "LoadStrategy",
                table: "ChargeControllers",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "HighLevelMeasuringDeviceControllerId",
                table: "ChargeControllers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HighLevelMeasuringDeviceModbus",
                table: "ChargeControllers",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HighLevelMeasuringDeviceControllerId",
                table: "ChargeControllers");

            migrationBuilder.DropColumn(
                name: "HighLevelMeasuringDeviceModbus",
                table: "ChargeControllers");

            migrationBuilder.AlterColumn<int>(
                name: "LoadStrategy",
                table: "ChargeControllers",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HighLevelMeasuringDevice",
                table: "ChargeControllers",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
