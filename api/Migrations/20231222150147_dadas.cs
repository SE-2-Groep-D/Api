using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class dadas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gebruikers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Voornaam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Achternaam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GoogleAccount = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gebruikers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hulpmiddelen",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Naam = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hulpmiddelen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeBeperkingen",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeBeperkingen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Voogden",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voogden", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Voorkeurbenaderingen",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voorkeurbenaderingen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_Gebruikers_UserId",
                        column: x => x.UserId,
                        principalTable: "Gebruikers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_Gebruikers_UserId",
                        column: x => x.UserId,
                        principalTable: "Gebruikers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_Gebruikers_UserId",
                        column: x => x.UserId,
                        principalTable: "Gebruikers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_Gebruikers_UserId",
                        column: x => x.UserId,
                        principalTable: "Gebruikers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bedrijven",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NaamBedrijf = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Postcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Plaats = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nummer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WebsiteUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Omschrijving = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bedrijven", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bedrijven_Gebruikers_Id",
                        column: x => x.Id,
                        principalTable: "Gebruikers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Medewerkers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Functie = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medewerkers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medewerkers_Gebruikers_Id",
                        column: x => x.Id,
                        principalTable: "Gebruikers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ervaringsdeskundigen",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Postcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToestemmingBenadering = table.Column<bool>(type: "bit", nullable: false),
                    Leeftijdscategorie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VoogdId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ervaringsdeskundigen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ervaringsdeskundigen_Gebruikers_Id",
                        column: x => x.Id,
                        principalTable: "Gebruikers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ervaringsdeskundigen_Voogden_VoogdId",
                        column: x => x.VoogdId,
                        principalTable: "Voogden",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Onderzoeken",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Omschrijving = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vergoeding = table.Column<double>(type: "float", nullable: false),
                    Locatie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BedrijfId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Onderzoeken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Onderzoeken_Bedrijven_BedrijfId",
                        column: x => x.BedrijfId,
                        principalTable: "Bedrijven",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Beschikbaarheden",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BeginDatumTijd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EindDatumTijd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ErvaringsdeskundigeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beschikbaarheden", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Beschikbaarheden_Ervaringsdeskundigen_ErvaringsdeskundigeId",
                        column: x => x.ErvaringsdeskundigeId,
                        principalTable: "Ervaringsdeskundigen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ErvaringsdeskundigeHulpmiddel",
                columns: table => new
                {
                    ErvaringsdeskundigenId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                        name: "FK_ErvaringsdeskundigeHulpmiddel_Hulpmiddelen_HulpmiddelenId",
                        column: x => x.HulpmiddelenId,
                        principalTable: "Hulpmiddelen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ErvaringsdeskundigeTypeBeperking",
                columns: table => new
                {
                    ErvaringsdeskundigenId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                        name: "FK_ErvaringsdeskundigeTypeBeperking_TypeBeperkingen_TypeBeperkingenId",
                        column: x => x.TypeBeperkingenId,
                        principalTable: "TypeBeperkingen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ErvaringsdeskundigeVoorkeurbenadering",
                columns: table => new
                {
                    ErvaringsdeskundigenId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                        name: "FK_ErvaringsdeskundigeVoorkeurbenadering_Voorkeurbenaderingen_VoorkeurbenaderingenId",
                        column: x => x.VoorkeurbenaderingenId,
                        principalTable: "Voorkeurbenaderingen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OnderzoekErvaringsdekundigen",
                columns: table => new
                {
                    OnderzoekId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ErvaringsdeskundigeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    datum = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnderzoekErvaringsdekundigen", x => new { x.ErvaringsdeskundigeId, x.OnderzoekId });
                    table.ForeignKey(
                        name: "FK_OnderzoekErvaringsdekundigen_Ervaringsdeskundigen_ErvaringsdeskundigeId",
                        column: x => x.ErvaringsdeskundigeId,
                        principalTable: "Ervaringsdeskundigen",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OnderzoekErvaringsdekundigen_Onderzoeken_OnderzoekId",
                        column: x => x.OnderzoekId,
                        principalTable: "Onderzoeken",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Vragenlijsten",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Samenvatting = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OnderzoekId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vragenlijsten", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vragenlijsten_Onderzoeken_OnderzoekId",
                        column: x => x.OnderzoekId,
                        principalTable: "Onderzoeken",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vragen",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Onderwerp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VragenlijstId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vragen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vragen_Vragenlijsten_VragenlijstId",
                        column: x => x.VragenlijstId,
                        principalTable: "Vragenlijsten",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Antwoorden",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tekst = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VraagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Antwoorden", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Antwoorden_Vragen_VraagId",
                        column: x => x.VraagId,
                        principalTable: "Vragen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("40de5fb2-052b-43df-8f1d-f14e40d4e663"), "40de5fb2-052b-43df-8f1d-f14e40d4e663", "Beheerder", "BEHEERDER" },
                    { new Guid("7f13d193-aa0b-4e0f-905a-fddc7ba1e8ef"), "7f13d193-aa0b-4e0f-905a-fddc7ba1e8ef", "Bedrijf", "BEDRIJF" },
                    { new Guid("ab6b8e6f-ca39-4d40-b330-e5898a785899"), "ab6b8e6f-ca39-4d40-b330-e5898a785899", "Ervaringsdeskundige", "ERVARINGSDESKUNDIGE" },
                    { new Guid("bb649c16-7c95-4319-9f00-9e1f7beade43"), "bb649c16-7c95-4319-9f00-9e1f7beade43", "Medewerker", "MEDEWERKER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Antwoorden_VraagId",
                table: "Antwoorden",
                column: "VraagId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Beschikbaarheden_ErvaringsdeskundigeId",
                table: "Beschikbaarheden",
                column: "ErvaringsdeskundigeId");

            migrationBuilder.CreateIndex(
                name: "IX_ErvaringsdeskundigeHulpmiddel_HulpmiddelenId",
                table: "ErvaringsdeskundigeHulpmiddel",
                column: "HulpmiddelenId");

            migrationBuilder.CreateIndex(
                name: "IX_Ervaringsdeskundigen_VoogdId",
                table: "Ervaringsdeskundigen",
                column: "VoogdId");

            migrationBuilder.CreateIndex(
                name: "IX_ErvaringsdeskundigeTypeBeperking_TypeBeperkingenId",
                table: "ErvaringsdeskundigeTypeBeperking",
                column: "TypeBeperkingenId");

            migrationBuilder.CreateIndex(
                name: "IX_ErvaringsdeskundigeVoorkeurbenadering_VoorkeurbenaderingenId",
                table: "ErvaringsdeskundigeVoorkeurbenadering",
                column: "VoorkeurbenaderingenId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Gebruikers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Gebruikers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Onderzoeken_BedrijfId",
                table: "Onderzoeken",
                column: "BedrijfId");

            migrationBuilder.CreateIndex(
                name: "IX_OnderzoekErvaringsdekundigen_OnderzoekId",
                table: "OnderzoekErvaringsdekundigen",
                column: "OnderzoekId");

            migrationBuilder.CreateIndex(
                name: "IX_Vragen_VragenlijstId",
                table: "Vragen",
                column: "VragenlijstId");

            migrationBuilder.CreateIndex(
                name: "IX_Vragenlijsten_OnderzoekId",
                table: "Vragenlijsten",
                column: "OnderzoekId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Antwoorden");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Beschikbaarheden");

            migrationBuilder.DropTable(
                name: "ErvaringsdeskundigeHulpmiddel");

            migrationBuilder.DropTable(
                name: "ErvaringsdeskundigeTypeBeperking");

            migrationBuilder.DropTable(
                name: "ErvaringsdeskundigeVoorkeurbenadering");

            migrationBuilder.DropTable(
                name: "Medewerkers");

            migrationBuilder.DropTable(
                name: "OnderzoekErvaringsdekundigen");

            migrationBuilder.DropTable(
                name: "Vragen");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Hulpmiddelen");

            migrationBuilder.DropTable(
                name: "TypeBeperkingen");

            migrationBuilder.DropTable(
                name: "Voorkeurbenaderingen");

            migrationBuilder.DropTable(
                name: "Ervaringsdeskundigen");

            migrationBuilder.DropTable(
                name: "Vragenlijsten");

            migrationBuilder.DropTable(
                name: "Voogden");

            migrationBuilder.DropTable(
                name: "Onderzoeken");

            migrationBuilder.DropTable(
                name: "Bedrijven");

            migrationBuilder.DropTable(
                name: "Gebruikers");
        }
    }
}
