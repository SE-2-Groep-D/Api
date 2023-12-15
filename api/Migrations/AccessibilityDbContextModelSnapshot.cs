﻿// <auto-generated />
using System;
using Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
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
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Api.Models.Domain.Gebruiker", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Achternaam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("GoogleAccount")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Voornaam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("Gebruikers", (string)null);

                    b.UseTptMappingStrategy();
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

                    b.ToTable("Hulpmiddel");
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

                    b.ToTable("TypeBeperking");
                });

            modelBuilder.Entity("Api.Models.Domain.Voogd", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Voogd");
                });

            modelBuilder.Entity("Api.Models.Domain.Voorkeurbenadering", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Voorkeurbenadering");
                });

            modelBuilder.Entity("ErvaringsdeskundigeHulpmiddel", b =>
                {
                    b.Property<string>("ErvaringsdeskundigenId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("HulpmiddelenId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ErvaringsdeskundigenId", "HulpmiddelenId");

                    b.HasIndex("HulpmiddelenId");

                    b.ToTable("ErvaringsdeskundigeHulpmiddel");
                });

            modelBuilder.Entity("ErvaringsdeskundigeTypeBeperking", b =>
                {
                    b.Property<string>("ErvaringsdeskundigenId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("TypeBeperkingenId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ErvaringsdeskundigenId", "TypeBeperkingenId");

                    b.HasIndex("TypeBeperkingenId");

                    b.ToTable("ErvaringsdeskundigeTypeBeperking");
                });

            modelBuilder.Entity("ErvaringsdeskundigeVoorkeurbenadering", b =>
                {
                    b.Property<string>("ErvaringsdeskundigenId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("VoorkeurbenaderingenId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ErvaringsdeskundigenId", "VoorkeurbenaderingenId");

                    b.HasIndex("VoorkeurbenaderingenId");

                    b.ToTable("ErvaringsdeskundigeVoorkeurbenadering");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "40de5fb2-052b-43df-8f1d-f14e40d4e663",
                            ConcurrencyStamp = "40de5fb2-052b-43df-8f1d-f14e40d4e663",
                            Name = "Beheerder",
                            NormalizedName = "BEHEERDER"
                        },
                        new
                        {
                            Id = "ab6b8e6f-ca39-4d40-b330-e5898a785899",
                            ConcurrencyStamp = "ab6b8e6f-ca39-4d40-b330-e5898a785899",
                            Name = "Ervaringsdeskundige",
                            NormalizedName = "ERVARINGSDESKUNDIGE"
                        },
                        new
                        {
                            Id = "7f13d193-aa0b-4e0f-905a-fddc7ba1e8ef",
                            ConcurrencyStamp = "7f13d193-aa0b-4e0f-905a-fddc7ba1e8ef",
                            Name = "Bedrijf",
                            NormalizedName = "BEDRIJF"
                        },
                        new
                        {
                            Id = "bb649c16-7c95-4319-9f00-9e1f7beade43",
                            ConcurrencyStamp = "bb649c16-7c95-4319-9f00-9e1f7beade43",
                            Name = "Medewerker",
                            NormalizedName = "MEDEWERKER"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Api.Models.Domain.Bedrijf", b =>
                {
                    b.HasBaseType("Api.Models.Domain.Gebruiker");

                    b.Property<string>("NaamBedrijf")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nummer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Omschrijving")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Plaats")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Postcode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WebsiteUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Bedrijven", (string)null);
                });

            modelBuilder.Entity("Api.Models.Domain.Ervaringsdeskundige", b =>
                {
                    b.HasBaseType("Api.Models.Domain.Gebruiker");

                    b.Property<string>("Leeftijdscategorie")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Postcode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ToestemmingBenadering")
                        .HasColumnType("bit");

                    b.Property<Guid?>("VoogdId")
                        .HasColumnType("uniqueidentifier");

                    b.HasIndex("VoogdId");

                    b.ToTable("Ervaringsdeskundigen", (string)null);
                });

            modelBuilder.Entity("Api.Models.Domain.Medewerker", b =>
                {
                    b.HasBaseType("Api.Models.Domain.Gebruiker");

                    b.Property<string>("Functie")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Medewerkers", (string)null);
                });

            modelBuilder.Entity("ErvaringsdeskundigeHulpmiddel", b =>
                {
                    b.HasOne("Api.Models.Domain.Ervaringsdeskundige", null)
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
                    b.HasOne("Api.Models.Domain.Ervaringsdeskundige", null)
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
                    b.HasOne("Api.Models.Domain.Ervaringsdeskundige", null)
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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Api.Models.Domain.Gebruiker", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Api.Models.Domain.Gebruiker", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Api.Models.Domain.Gebruiker", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Api.Models.Domain.Gebruiker", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Api.Models.Domain.Bedrijf", b =>
                {
                    b.HasOne("Api.Models.Domain.Gebruiker", null)
                        .WithOne()
                        .HasForeignKey("Api.Models.Domain.Bedrijf", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Api.Models.Domain.Ervaringsdeskundige", b =>
                {
                    b.HasOne("Api.Models.Domain.Gebruiker", null)
                        .WithOne()
                        .HasForeignKey("Api.Models.Domain.Ervaringsdeskundige", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Api.Models.Domain.Voogd", "Voogd")
                        .WithMany("Ervaringsdeskundigen")
                        .HasForeignKey("VoogdId");

                    b.Navigation("Voogd");
                });

            modelBuilder.Entity("Api.Models.Domain.Medewerker", b =>
                {
                    b.HasOne("Api.Models.Domain.Gebruiker", null)
                        .WithOne()
                        .HasForeignKey("Api.Models.Domain.Medewerker", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Api.Models.Domain.Voogd", b =>
                {
                    b.Navigation("Ervaringsdeskundigen");
                });
#pragma warning restore 612, 618
        }
    }
}
