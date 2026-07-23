using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SimpleCarDb.Migrations
{
    /// <inheritdoc />
    public partial class FinalSync : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EngineDetails_Cars_CarId",
                table: "EngineDetails");

            migrationBuilder.DropIndex(
                name: "IX_EngineDetails_CarId",
                table: "EngineDetails");

            migrationBuilder.DeleteData(
                table: "EngineDetails",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EngineDetails",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EngineDetails",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EngineDetails",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "EngineDetails",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "EngineDetails",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "EngineDetails",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "EngineDetails",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "EngineDetails",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "EngineDetails",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.AlterColumn<int>(
                name: "CarId",
                table: "EngineDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_EngineDetails_CarId",
                table: "EngineDetails",
                column: "CarId",
                unique: true,
                filter: "[CarId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_EngineDetails_Cars_CarId",
                table: "EngineDetails",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EngineDetails_Cars_CarId",
                table: "EngineDetails");

            migrationBuilder.DropIndex(
                name: "IX_EngineDetails_CarId",
                table: "EngineDetails");

            migrationBuilder.AlterColumn<int>(
                name: "CarId",
                table: "EngineDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "EngineDetails",
                columns: new[] { "Id", "CapacityCc", "CarId", "EngineNumber", "Horsepower" },
                values: new object[,]
                {
                    { 1, 1995, 1, "B47D20A123456", 190 },
                    { 2, 2993, 2, "S58B30A654321", 510 },
                    { 3, 1798, 3, "2ZRFXE987654", 122 },
                    { 4, 2487, 4, "A25AFXS456789", 306 },
                    { 5, 5038, 5, "COY50V8112233", 450 },
                    { 6, 3496, 6, "ECO35V6998877", 400 },
                    { 7, 1968, 7, "DEUA20TDI3344", 204 },
                    { 8, 3996, 8, "DBCV40TFSI667", 600 },
                    { 9, 0, 9, "EM0004ELEC112", 292 },
                    { 10, 1993, 10, "OM65420D99001", 200 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EngineDetails_CarId",
                table: "EngineDetails",
                column: "CarId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EngineDetails_Cars_CarId",
                table: "EngineDetails",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
