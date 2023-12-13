using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class M4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "googleAccount",
                table: "Gebruikers",
                newName: "GoogleAccount");

            migrationBuilder.AddColumn<string>(
                name: "Leeftijdscategorie",
                table: "Ervaringsdeskundigen",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "ToestemmingBenadering",
                table: "Ervaringsdeskundigen",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Nummer",
                table: "Bedrijven",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Omschrijving",
                table: "Bedrijven",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Plaats",
                table: "Bedrijven",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Postcode",
                table: "Bedrijven",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WebsiteUrl",
                table: "Bedrijven",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Leeftijdscategorie",
                table: "Ervaringsdeskundigen");

            migrationBuilder.DropColumn(
                name: "ToestemmingBenadering",
                table: "Ervaringsdeskundigen");

            migrationBuilder.DropColumn(
                name: "Nummer",
                table: "Bedrijven");

            migrationBuilder.DropColumn(
                name: "Omschrijving",
                table: "Bedrijven");

            migrationBuilder.DropColumn(
                name: "Plaats",
                table: "Bedrijven");

            migrationBuilder.DropColumn(
                name: "Postcode",
                table: "Bedrijven");

            migrationBuilder.DropColumn(
                name: "WebsiteUrl",
                table: "Bedrijven");

            migrationBuilder.RenameColumn(
                name: "GoogleAccount",
                table: "Gebruikers",
                newName: "googleAccount");
        }
    }
}
