﻿// <auto-generated />
using System;
using Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Api.Migrations
{
    [DbContext(typeof(AccessibilityDbContext))]
    partial class AccessibilityDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Api.Models.Domain.Beschikbaarheid", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("BeginDatumTijd")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("EindDatumTijd")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("ErvaringsdeskundigeId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("ErvaringsdeskundigeId");

                    b.ToTable("Beschikbaarheden");
                });

            modelBuilder.Entity("Api.Models.Domain.Hulpmiddel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()

                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");
                    b.HasKey("Id");

                    b.ToTable("Hulpmiddelen");
                });


            modelBuilder.Entity("Api.Models.Domain.OnderzoekErvaringsdekundige", b =>
                {
                    b.Property<Guid>("ErvaringsdeskundigeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OnderzoekId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("datum")
                        .HasColumnType("datetime2");

                    b.HasKey("ErvaringsdeskundigeId", "OnderzoekId");

                    b.HasIndex("OnderzoekId");

                    b.ToTable("OnderzoekErvaringsdekundigen");
                });

            modelBuilder.Entity("Api.Models.Domain.Research.Antwoord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Tekst")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("VraagId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("VraagtId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("VraagId");

                    b.ToTable("Antwoorden");
                });

            modelBuilder.Entity("Api.Models.Domain.Research.Onderzoek", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BedrijfId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Locatie")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Omschrijving")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDatum")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Vergoeding")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("BedrijfId");

                    b.ToTable("Onderzoeken");
                });

            modelBuilder.Entity("Api.Models.Domain.Research.Vraag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Onderwerp")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("VragenlijstId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("VragenlijstId");

                    b.ToTable("Vragen");
                });

            modelBuilder.Entity("Api.Models.Domain.Research.Vragenlijst", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OnderzoekId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Samenvatting")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Titel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OnderzoekId");

                    b.ToTable("Vragenlijsten");
                });

            modelBuilder.Entity("Api.Models.Domain.TypeBeperking", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()

                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");


                    b.HasKey("Id");

                    b.ToTable("TypeBeperkingen");
                });

            modelBuilder.Entity("Api.Models.Domain.User.Gebruiker", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Achternaam")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("GoogleAccount")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Voornaam")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("Gebruikers", (string)null);

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("Api.Models.Domain.Voogd", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Voogden");
                });

            modelBuilder.Entity("Api.Models.Domain.Voorkeurbenadering", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Voorkeurbenaderingen");
                });

            modelBuilder.Entity("ErvaringsdeskundigeHulpmiddel", b =>
                {
                    b.Property<Guid>("ErvaringsdeskundigenId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("HulpmiddelenId")
                        .HasColumnType("char(36)");

                    b.HasKey("ErvaringsdeskundigenId", "HulpmiddelenId");

                    b.HasIndex("HulpmiddelenId");

                    b.ToTable("ErvaringsdeskundigeHulpmiddel");
                });

            modelBuilder.Entity("ErvaringsdeskundigeTypeBeperking", b =>
                {
                    b.Property<Guid>("ErvaringsdeskundigenId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("TypeBeperkingenId")
                        .HasColumnType("char(36)");

                    b.HasKey("ErvaringsdeskundigenId", "TypeBeperkingenId");

                    b.HasIndex("TypeBeperkingenId");

                    b.ToTable("ErvaringsdeskundigeTypeBeperking");
                });

            modelBuilder.Entity("ErvaringsdeskundigeVoorkeurbenadering", b =>
                {
                    b.Property<Guid>("ErvaringsdeskundigenId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("VoorkeurbenaderingenId")
                        .HasColumnType("char(36)");

                    b.HasKey("ErvaringsdeskundigenId", "VoorkeurbenaderingenId");

                    b.HasIndex("VoorkeurbenaderingenId");

                    b.ToTable("ErvaringsdeskundigeVoorkeurbenadering");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("40de5fb2-052b-43df-8f1d-f14e40d4e663"),
                            ConcurrencyStamp = "40de5fb2-052b-43df-8f1d-f14e40d4e663",
                            Name = "Beheerder",
                            NormalizedName = "BEHEERDER"
                        },
                        new
                        {
                            Id = new Guid("ab6b8e6f-ca39-4d40-b330-e5898a785899"),
                            ConcurrencyStamp = "ab6b8e6f-ca39-4d40-b330-e5898a785899",
                            Name = "Ervaringsdeskundige",
                            NormalizedName = "ERVARINGSDESKUNDIGE"
                        },
                        new
                        {
                            Id = new Guid("7f13d193-aa0b-4e0f-905a-fddc7ba1e8ef"),
                            ConcurrencyStamp = "7f13d193-aa0b-4e0f-905a-fddc7ba1e8ef",
                            Name = "Bedrijf",
                            NormalizedName = "BEDRIJF"
                        },
                        new
                        {
                            Id = new Guid("bb649c16-7c95-4319-9f00-9e1f7beade43"),
                            ConcurrencyStamp = "bb649c16-7c95-4319-9f00-9e1f7beade43",
                            Name = "Medewerker",
                            NormalizedName = "MEDEWERKER"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("char(36)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Api.Models.Domain.User.Bedrijf", b =>
                {
                    b.HasBaseType("Api.Models.Domain.User.Gebruiker");

                    b.Property<string>("NaamBedrijf")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nummer")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Omschrijving")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Plaats")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Postcode")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("WebsiteUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.ToTable("Bedrijven", (string)null);
                });

            modelBuilder.Entity("Api.Models.Domain.User.Ervaringsdeskundige", b =>
                {
                    b.HasBaseType("Api.Models.Domain.User.Gebruiker");

                    b.Property<string>("Leeftijdscategorie")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Postcode")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("ToestemmingBenadering")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid?>("VoogdId")
                        .HasColumnType("char(36)");

                    b.HasIndex("VoogdId");

                    b.ToTable("Ervaringsdeskundigen", (string)null);
                });

            modelBuilder.Entity("Api.Models.Domain.User.Medewerker", b =>
                {
                    b.HasBaseType("Api.Models.Domain.User.Gebruiker");

                    b.Property<string>("Functie")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.ToTable("Medewerkers", (string)null);
                });

            modelBuilder.Entity("Api.Models.Domain.Beschikbaarheid", b =>
                {
                    b.HasOne("Api.Models.Domain.User.Ervaringsdeskundige", "Ervaringsdeskundige")
                        .WithMany("Beschikbaarheden")
                        .HasForeignKey("ErvaringsdeskundigeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ervaringsdeskundige");
                });

            modelBuilder.Entity("Api.Models.Domain.OnderzoekErvaringsdekundige", b =>
                {
                    b.HasOne("Api.Models.Domain.User.Ervaringsdeskundige", "Ervaringsdeskundige")
                        .WithMany("OnderzoekErvaringsdekundigen")
                        .HasForeignKey("ErvaringsdeskundigeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Api.Models.Domain.Research.Onderzoek", "Onderzoek")
                        .WithMany("OnderzoekErvaringsdekundigen")
                        .HasForeignKey("OnderzoekId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Ervaringsdeskundige");

                    b.Navigation("Onderzoek");
                });

            modelBuilder.Entity("Api.Models.Domain.Research.Antwoord", b =>
                {
                    b.HasOne("Api.Models.Domain.Research.Vraag", "Vraag")
                        .WithMany("Antwoorden")
                        .HasForeignKey("VraagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vraag");
                });

            modelBuilder.Entity("Api.Models.Domain.Research.Onderzoek", b =>
                {
                    b.HasOne("Api.Models.Domain.User.Bedrijf", "Bedrijf")
                        .WithMany()
                        .HasForeignKey("BedrijfId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bedrijf");
                });

            modelBuilder.Entity("Api.Models.Domain.Research.Vraag", b =>
                {
                    b.HasOne("Api.Models.Domain.Research.Vragenlijst", "Vragenlijst")
                        .WithMany("Vragen")
                        .HasForeignKey("VragenlijstId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vragenlijst");
                });

            modelBuilder.Entity("Api.Models.Domain.Research.Vragenlijst", b =>
                {
                    b.HasOne("Api.Models.Domain.Research.Onderzoek", "Onderzoek")
                        .WithMany("Vragenlijst")
                        .HasForeignKey("OnderzoekId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Onderzoek");
                });

            modelBuilder.Entity("ErvaringsdeskundigeHulpmiddel", b =>
                {
                    b.HasOne("Api.Models.Domain.User.Ervaringsdeskundige", null)
                        .WithMany()
                        .HasForeignKey("ErvaringsdeskundigenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Api.Models.Domain.Hulpmiddel", null)
                        .WithMany()
                        .HasForeignKey("HulpmiddelenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ErvaringsdeskundigeTypeBeperking", b =>
                {
                    b.HasOne("Api.Models.Domain.User.Ervaringsdeskundige", null)
                        .WithMany()
                        .HasForeignKey("ErvaringsdeskundigenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Api.Models.Domain.TypeBeperking", null)
                        .WithMany()
                        .HasForeignKey("TypeBeperkingenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ErvaringsdeskundigeVoorkeurbenadering", b =>
                {
                    b.HasOne("Api.Models.Domain.User.Ervaringsdeskundige", null)
                        .WithMany()
                        .HasForeignKey("ErvaringsdeskundigenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Api.Models.Domain.Voorkeurbenadering", null)
                        .WithMany()
                        .HasForeignKey("VoorkeurbenaderingenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("Api.Models.Domain.User.Gebruiker", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("Api.Models.Domain.User.Gebruiker", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Api.Models.Domain.User.Gebruiker", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("Api.Models.Domain.User.Gebruiker", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Api.Models.Domain.User.Bedrijf", b =>
                {
                    b.HasOne("Api.Models.Domain.User.Gebruiker", null)
                        .WithOne()
                        .HasForeignKey("Api.Models.Domain.User.Bedrijf", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Api.Models.Domain.User.Ervaringsdeskundige", b =>
                {
                    b.HasOne("Api.Models.Domain.User.Gebruiker", null)
                        .WithOne()
                        .HasForeignKey("Api.Models.Domain.User.Ervaringsdeskundige", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Api.Models.Domain.Voogd", "Voogd")
                        .WithMany("Ervaringsdeskundigen")
                        .HasForeignKey("VoogdId");

                    b.Navigation("Voogd");
                });

            modelBuilder.Entity("Api.Models.Domain.User.Medewerker", b =>
                {
                    b.HasOne("Api.Models.Domain.User.Gebruiker", null)
                        .WithOne()
                        .HasForeignKey("Api.Models.Domain.User.Medewerker", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Api.Models.Domain.Research.Onderzoek", b =>
                {
                    b.Navigation("OnderzoekErvaringsdekundigen");

                    b.Navigation("Vragenlijst");
                });

            modelBuilder.Entity("Api.Models.Domain.Research.Vraag", b =>
                {
                    b.Navigation("Antwoorden");
                });

            modelBuilder.Entity("Api.Models.Domain.Research.Vragenlijst", b =>
                {
                    b.Navigation("Vragen");
                });

            modelBuilder.Entity("Api.Models.Domain.Voogd", b =>
                {
                    b.Navigation("Ervaringsdeskundigen");
                });

            modelBuilder.Entity("Api.Models.Domain.User.Ervaringsdeskundige", b =>
                {
                    b.Navigation("Beschikbaarheden");

                    b.Navigation("OnderzoekErvaringsdekundigen");
                });
#pragma warning restore 612, 618
        }
    }
}
