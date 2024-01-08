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

            modelBuilder.Entity("Api.Models.Domain.Beschikbaarheid", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("BeginDatumTijd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EindDatumTijd")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ErvaringsdeskundigeId")
                        .HasColumnType("uniqueidentifier");

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

            modelBuilder.Entity("Api.Models.Domain.Research.Answer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Answertext")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("GivenAnswerQuestionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PossibleAnswerQuestionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("GivenAnswerQuestionId");

                    b.HasIndex("PossibleAnswerQuestionId");

                    b.ToTable("Answer");
                });

            modelBuilder.Entity("Api.Models.Domain.Research.Onderzoek", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AantalParticipanten")
                        .HasColumnType("int");

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

                    b.Property<string>("Titel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Vergoeding")
                        .HasColumnType("float");

                    b.Property<string>("websiteUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BedrijfId");

                    b.ToTable("Onderzoeken");
                });

            modelBuilder.Entity("Api.Models.Domain.Research.Question", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("QuestionlistId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("QuestionlistId");

                    b.ToTable("Question");
                });

            modelBuilder.Entity("Api.Models.Domain.Research.Questionlist", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OnderzoekId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("Participants")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TotalAwnsers")
                        .HasColumnType("int");

                    b.Property<int?>("TotalQuestions")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OnderzoekId");

                    b.ToTable("Questionlist");
                });

            modelBuilder.Entity("Api.Models.Domain.Research.Tracking.ClickedItem", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Href")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TimeInSeconds")
                        .HasColumnType("int");

                    b.Property<Guid?>("TrackingResultatenId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("itemType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("TrackingResultatenId");

                    b.ToTable("ClickedItem");
                });

            modelBuilder.Entity("Api.Models.Domain.Research.Tracking.TrackingOnderzoek", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Domain")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OnderzoekId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("OnderzoekId");

                    b.ToTable("TrackingOnderzoeken");
                });

            modelBuilder.Entity("Api.Models.Domain.Research.Tracking.TrackingResultaten", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Browser")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Page")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PagePercentage")
                        .HasColumnType("int");

                    b.Property<int>("TimeInSeconds")
                        .HasColumnType("int");

                    b.Property<int>("TimeTillAction")
                        .HasColumnType("int");

                    b.Property<Guid?>("TrackingOnderzoekId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TrackingOnderzoekId");

                    b.ToTable("TrackingResultaten");
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
                        .HasColumnType("uniqueidentifier");

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

                    b.Property<string>("Phonenumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Voogden");
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

                    b.ToTable("Voorkeurbenaderingen");
                });

            modelBuilder.Entity("ErvaringsdeskundigeHulpmiddel", b =>
                {
                    b.Property<Guid>("ErvaringsdeskundigenId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("HulpmiddelenId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ErvaringsdeskundigenId", "HulpmiddelenId");

                    b.HasIndex("HulpmiddelenId");

                    b.ToTable("ErvaringsdeskundigeHulpmiddel");
                });

            modelBuilder.Entity("ErvaringsdeskundigeTypeBeperking", b =>
                {
                    b.Property<Guid>("ErvaringsdeskundigenId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TypeBeperkingenId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ErvaringsdeskundigenId", "TypeBeperkingenId");

                    b.HasIndex("TypeBeperkingenId");

                    b.ToTable("ErvaringsdeskundigeTypeBeperking");
                });

            modelBuilder.Entity("ErvaringsdeskundigeVoorkeurbenadering", b =>
                {
                    b.Property<Guid>("ErvaringsdeskundigenId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("VoorkeurbenaderingenId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ErvaringsdeskundigenId", "VoorkeurbenaderingenId");

                    b.HasIndex("VoorkeurbenaderingenId");

                    b.ToTable("ErvaringsdeskundigeVoorkeurbenadering");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

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

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Nieuwsbrief", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Datum")
                        .HasColumnType("datetime2");

                    b.Property<string>("Inhoud")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("MedewerkerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Titel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MedewerkerId");

                    b.ToTable("Nieuws");
                });

            modelBuilder.Entity("Api.Models.Domain.User.Bedrijf", b =>
                {
                    b.HasBaseType("Api.Models.Domain.User.Gebruiker");

                    b.Property<string>("Bedrijfsnaam")
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

                    b.Property<string>("Straat")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WebsiteUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Bedrijven", (string)null);
                });

            modelBuilder.Entity("Api.Models.Domain.User.Ervaringsdeskundige", b =>
                {
                    b.HasBaseType("Api.Models.Domain.User.Gebruiker");

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

            modelBuilder.Entity("Api.Models.Domain.User.Medewerker", b =>
                {
                    b.HasBaseType("Api.Models.Domain.User.Gebruiker");

                    b.Property<string>("Functie")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

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

            modelBuilder.Entity("Api.Models.Domain.Research.Answer", b =>
                {
                    b.HasOne("Api.Models.Domain.Research.Question", "QuestionAsGivenAnswer")
                        .WithMany("GivenAnswers")
                        .HasForeignKey("GivenAnswerQuestionId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Api.Models.Domain.Research.Question", "QuestionAsPossibleAnswer")
                        .WithMany("PossibleAnswers")
                        .HasForeignKey("PossibleAnswerQuestionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("QuestionAsGivenAnswer");

                    b.Navigation("QuestionAsPossibleAnswer");
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

            modelBuilder.Entity("Api.Models.Domain.Research.Question", b =>
                {
                    b.HasOne("Api.Models.Domain.Research.Questionlist", "Questionlist")
                        .WithMany("Questions")
                        .HasForeignKey("QuestionlistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Questionlist");
                });

            modelBuilder.Entity("Api.Models.Domain.Research.Questionlist", b =>
                {
                    b.HasOne("Api.Models.Domain.Research.Onderzoek", "Onderzoek")
                        .WithMany("Vragenlijst")
                        .HasForeignKey("OnderzoekId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Onderzoek");
                });

            modelBuilder.Entity("Api.Models.Domain.Research.Tracking.ClickedItem", b =>
                {
                    b.HasOne("Api.Models.Domain.Research.Tracking.TrackingResultaten", null)
                        .WithMany("ClickedItems")
                        .HasForeignKey("TrackingResultatenId");
                });

            modelBuilder.Entity("Api.Models.Domain.Research.Tracking.TrackingOnderzoek", b =>
                {
                    b.HasOne("Api.Models.Domain.Research.Onderzoek", null)
                        .WithMany("TrackingResultaten")
                        .HasForeignKey("OnderzoekId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Api.Models.Domain.Research.Tracking.TrackingResultaten", b =>
                {
                    b.HasOne("Api.Models.Domain.Research.Tracking.TrackingOnderzoek", null)
                        .WithMany("TrackingResultaten")
                        .HasForeignKey("TrackingOnderzoekId");
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

            modelBuilder.Entity("Nieuwsbrief", b =>
                {
                    b.HasOne("Api.Models.Domain.User.Medewerker", "Medewerker")
                        .WithMany()
                        .HasForeignKey("MedewerkerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Medewerker");
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

                    b.Navigation("TrackingResultaten");

                    b.Navigation("Vragenlijst");
                });

            modelBuilder.Entity("Api.Models.Domain.Research.Question", b =>
                {
                    b.Navigation("GivenAnswers");

                    b.Navigation("PossibleAnswers");
                });

            modelBuilder.Entity("Api.Models.Domain.Research.Questionlist", b =>
                {
                    b.Navigation("Questions");
                });

            modelBuilder.Entity("Api.Models.Domain.Research.Tracking.TrackingOnderzoek", b =>
                {
                    b.Navigation("TrackingResultaten");
                });

            modelBuilder.Entity("Api.Models.Domain.Research.Tracking.TrackingResultaten", b =>
                {
                    b.Navigation("ClickedItems");
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
