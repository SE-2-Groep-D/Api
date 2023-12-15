using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class M7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "VoogdId",
                table: "Ervaringsdeskundigen",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Hulpmiddel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Naam = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hulpmiddel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeBeperking",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeBeperking", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Voogd",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voogd", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Voorkeurbenadering",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voorkeurbenadering", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ErvaringsdeskundigeHulpmiddel",
                columns: table => new
                {
                    ErvaringsdeskundigenId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HulpmiddelenId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErvaringsdeskundigeHulpmiddel", x => new { x.ErvaringsdeskundigenId, x.HulpmiddelenId });
                    table.ForeignKey(
                        name: "FK_ErvaringsdeskundigeHulpmiddel_Ervaringsdeskundigen_ErvaringsdeskundigenId",
                        column: x => x.ErvaringsdeskundigenId,
                        principalTable: "Ervaringsdeskundigen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ErvaringsdeskundigeHulpmiddel_Hulpmiddel_HulpmiddelenId",
                        column: x => x.HulpmiddelenId,
                        principalTable: "Hulpmiddel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ErvaringsdeskundigeTypeBeperking",
                columns: table => new
                {
                    ErvaringsdeskundigenId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TypeBeperkingenId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErvaringsdeskundigeTypeBeperking", x => new { x.ErvaringsdeskundigenId, x.TypeBeperkingenId });
                    table.ForeignKey(
                        name: "FK_ErvaringsdeskundigeTypeBeperking_Ervaringsdeskundigen_ErvaringsdeskundigenId",
                        column: x => x.ErvaringsdeskundigenId,
                        principalTable: "Ervaringsdeskundigen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ErvaringsdeskundigeTypeBeperking_TypeBeperking_TypeBeperkingenId",
                        column: x => x.TypeBeperkingenId,
                        principalTable: "TypeBeperking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ErvaringsdeskundigeVoorkeurbenadering",
                columns: table => new
                {
                    ErvaringsdeskundigenId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VoorkeurbenaderingenId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErvaringsdeskundigeVoorkeurbenadering", x => new { x.ErvaringsdeskundigenId, x.VoorkeurbenaderingenId });
                    table.ForeignKey(
                        name: "FK_ErvaringsdeskundigeVoorkeurbenadering_Ervaringsdeskundigen_ErvaringsdeskundigenId",
                        column: x => x.ErvaringsdeskundigenId,
                        principalTable: "Ervaringsdeskundigen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ErvaringsdeskundigeVoorkeurbenadering_Voorkeurbenadering_VoorkeurbenaderingenId",
                        column: x => x.VoorkeurbenaderingenId,
                        principalTable: "Voorkeurbenadering",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ervaringsdeskundigen_VoogdId",
                table: "Ervaringsdeskundigen",
                column: "VoogdId");

            migrationBuilder.CreateIndex(
                name: "IX_ErvaringsdeskundigeHulpmiddel_HulpmiddelenId",
                table: "ErvaringsdeskundigeHulpmiddel",
                column: "HulpmiddelenId");

            migrationBuilder.CreateIndex(
                name: "IX_ErvaringsdeskundigeTypeBeperking_TypeBeperkingenId",
                table: "ErvaringsdeskundigeTypeBeperking",
                column: "TypeBeperkingenId");

            migrationBuilder.CreateIndex(
                name: "IX_ErvaringsdeskundigeVoorkeurbenadering_VoorkeurbenaderingenId",
                table: "ErvaringsdeskundigeVoorkeurbenadering",
                column: "VoorkeurbenaderingenId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ervaringsdeskundigen_Voogd_VoogdId",
                table: "Ervaringsdeskundigen",
                column: "VoogdId",
                principalTable: "Voogd",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ervaringsdeskundigen_Voogd_VoogdId",
                table: "Ervaringsdeskundigen");

            migrationBuilder.DropTable(
                name: "ErvaringsdeskundigeHulpmiddel");

            migrationBuilder.DropTable(
                name: "ErvaringsdeskundigeTypeBeperking");

            migrationBuilder.DropTable(
                name: "ErvaringsdeskundigeVoorkeurbenadering");

            migrationBuilder.DropTable(
                name: "Voogd");

            migrationBuilder.DropTable(
                name: "Hulpmiddel");

            migrationBuilder.DropTable(
                name: "TypeBeperking");

            migrationBuilder.DropTable(
                name: "Voorkeurbenadering");

            migrationBuilder.DropIndex(
                name: "IX_Ervaringsdeskundigen_VoogdId",
                table: "Ervaringsdeskundigen");

            migrationBuilder.DropColumn(
                name: "VoogdId",
                table: "Ervaringsdeskundigen");
        }
    }
}
