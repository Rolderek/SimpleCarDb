using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleCarDb.Migrations
{
    public partial class MakeCarIdNullable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Kényszerítjük, hogy engedje a NULL-t
            migrationBuilder.AlterColumn<int>(
                name: "CarId",
                table: "EngineDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Visszacsinálás (ha valaha vissza kéne vonni a migrációt)
            migrationBuilder.AlterColumn<int>(
                name: "CarId",
                table: "EngineDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}