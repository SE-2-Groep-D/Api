﻿using Api.Models.Domain;
using Api.Models.Domain.Research;
using Api.Models.Domain.Research.Tracking;
using Api.Models.Domain.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Api.Data;
public class AccessibilityDbContext : IdentityDbContext<Gebruiker, IdentityRole<Guid>, Guid> {

  public AccessibilityDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

  public DbSet<Gebruiker> Gebruikers { get; set; }

  public DbSet<Ervaringsdeskundige> Ervaringsdeskundigen { get; set; }
  public DbSet<Voorkeurbenadering> Voorkeurbenaderingen { get; set; }
  public DbSet<Voogd> Voogden { get; set; }
  
  public DbSet<Hulpmiddel> Hulpmiddelen { get; set; }
  public DbSet<Beschikbaarheid> Beschikbaarheden { get; set; }


  public DbSet<Bedrijf> Bedrijven { get; set; }
  public DbSet<Medewerker> Medewerkers { get; set; }

  public DbSet<Nieuwsbrief> Nieuws { get; set; }

  public DbSet<Onderzoek> Onderzoeken { get; set; }
  public DbSet<OnderzoekErvaringsdekundige> OnderzoekErvaringsdekundigen { get; set; }

  public DbSet<Antwoord> Antwoorden { get; set; }
  public DbSet<Vraag> Vragen { get; set; }
  public DbSet<Vragenlijst> Vragenlijsten { get; set; }

  public DbSet<TrackingOnderzoek> TrackingOnderzoeken { get; set; }

  protected override void OnModelCreating(ModelBuilder builder) {
    base.OnModelCreating(builder);
    builder.Entity<Gebruiker>(entity => { entity.ToTable("Gebruikers"); });
    builder.Entity<Ervaringsdeskundige>(entity => { entity.ToTable("Ervaringsdeskundigen"); });
    builder.Entity<Bedrijf>(entity => { entity.ToTable("Bedrijven"); });
    builder.Entity<Medewerker>(entity => { entity.ToTable("Medewerkers"); });


    builder.Entity<Onderzoek>()
      .HasMany(e => e.Ervaringsdeskundigen)
      .WithMany(e => e.Onderzoeken)
      .UsingEntity<OnderzoekErvaringsdekundige>(
        j => j
          .HasOne(pt => pt.Ervaringsdeskundige)
          .WithMany(t => t.OnderzoekErvaringsdekundigen)
          .HasForeignKey(pt => pt.ErvaringsdeskundigeId)
          .OnDelete(DeleteBehavior.NoAction),
        j => j
          .HasOne(pt => pt.Onderzoek)
          .WithMany(p => p.OnderzoekErvaringsdekundigen)
          .HasForeignKey(pt => pt.OnderzoekId)
          .OnDelete(DeleteBehavior.NoAction));
    
    
    var beheerderRoleId = "40de5fb2-052b-43df-8f1d-f14e40d4e663";
    var ervaringsdeskundigeRoleId = "ab6b8e6f-ca39-4d40-b330-e5898a785899";
    var bedrijfRoleId = "7f13d193-aa0b-4e0f-905a-fddc7ba1e8ef";
    var medewerkerRoleId = "bb649c16-7c95-4319-9f00-9e1f7beade43";
    

    var roles = new List<IdentityRole<Guid>> {
      new IdentityRole<Guid> {
        Id = Guid.Parse(beheerderRoleId),
        ConcurrencyStamp = beheerderRoleId,
        Name = "Beheerder",
        NormalizedName = "Beheerder".ToUpper()
      },
      new IdentityRole<Guid> {
        Id = Guid.Parse(ervaringsdeskundigeRoleId),
        ConcurrencyStamp = ervaringsdeskundigeRoleId,
        Name = "Ervaringsdeskundige",
        NormalizedName = "Ervaringsdeskundige".ToUpper()
      },
      new IdentityRole<Guid> {
        Id = Guid.Parse(bedrijfRoleId),
        ConcurrencyStamp = bedrijfRoleId,
        Name = "Bedrijf",
        NormalizedName = "Bedrijf".ToUpper()
      },
      new IdentityRole<Guid> {
        Id = Guid.Parse(medewerkerRoleId),
        ConcurrencyStamp = medewerkerRoleId,
        Name = "Medewerker",
        NormalizedName = "Medewerker".ToUpper()
      },
    };

    builder.Entity<IdentityRole<Guid>>().HasData(roles);

    builder.Entity<Nieuwsbrief>()
      .HasOne(n => n.Medewerker)
      .WithMany()
      .HasForeignKey(n => n.MedewerkerId);
    
    
  }
  
  

}
