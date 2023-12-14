using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class M6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5d67df1b-5426-4158-aa57-7bc2d1030980");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "42c9bc45-9f69-4fc6-b0f8-b107fd5d6ae7", "42c9bc45-9f69-4fc6-b0f8-b107fd5d6ae7", "Medewerker", "MEDEWERKER" },
                    { "a8cf2a9d-3b76-40d5-8142-62d2110017d4", "a8cf2a9d-3b76-40d5-8142-62d2110017d4", "Beheerder", "BEHEERDER" },
                    { "b3184982-8537-46de-b065-bc59441c6b85", "b3184982-8537-46de-b065-bc59441c6b85", "Bedrijf", "BEDRIJF" },
                    { "e5414b41-0ce4-4d2c-889f-9594a8285fe5", "e5414b41-0ce4-4d2c-889f-9594a8285fe5", "Ervaringsdeskundige", "ERVARINGSDESKUNDIGE" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "42c9bc45-9f69-4fc6-b0f8-b107fd5d6ae7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a8cf2a9d-3b76-40d5-8142-62d2110017d4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b3184982-8537-46de-b065-bc59441c6b85");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e5414b41-0ce4-4d2c-889f-9594a8285fe5");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5d67df1b-5426-4158-aa57-7bc2d1030980", "5d67df1b-5426-4158-aa57-7bc2d1030980", "Beheerder", "BEHEERDER" });
        }
    }
}
