using Api.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class AccessibilityDbContext: IdentityDbContext<Gebruiker>
    {
        public AccessibilityDbContext(DbContextOptions dbContextOptions): base(dbContextOptions) { }

        public DbSet<Gebruiker> Gebruikers { get; set; }
        public DbSet<Ervaringsdeskundige> Ervaringsdeskundigen { get; set; }
        public DbSet<Bedrijf> Bedrijven { get; set; }
        public DbSet<Medewerker> Medewerkers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Gebruiker>(entity => { entity.ToTable("Gebruikers"); });
            builder.Entity<Ervaringsdeskundige>(entity => { entity.ToTable("Ervaringsdeskundigen"); });
            builder.Entity<Bedrijf>(entity => { entity.ToTable("Bedrijven"); });
            builder.Entity<Medewerker>(entity => { entity.ToTable("Medewerkers"); });


            var beheerderRoleId = "a8cf2a9d-3b76-40d5-8142-62d2110017d4";
            var ervaringsdeskundigeRoleId = "e5414b41-0ce4-4d2c-889f-9594a8285fe5";
            var bedrijfRoleId = "b3184982-8537-46de-b065-bc59441c6b85";
            var medewerkerRoleId = "42c9bc45-9f69-4fc6-b0f8-b107fd5d6ae7";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = beheerderRoleId,
                    ConcurrencyStamp = beheerderRoleId,
                    Name = "Beheerder",
                    NormalizedName = "Beheerder".ToUpper()
                },
                new IdentityRole
                {
                    Id = ervaringsdeskundigeRoleId,
                    ConcurrencyStamp = ervaringsdeskundigeRoleId,
                    Name = "Ervaringsdeskundige",
                    NormalizedName = "Ervaringsdeskundige".ToUpper()
                },
                new IdentityRole
                {
                    Id = bedrijfRoleId,
                    ConcurrencyStamp = bedrijfRoleId,
                    Name = "Bedrijf",
                    NormalizedName = "Bedrijf".ToUpper()
                },
                new IdentityRole
                {
                    Id = medewerkerRoleId,
                    ConcurrencyStamp = medewerkerRoleId,
                    Name = "Medewerker",
                    NormalizedName = "Medewerker".ToUpper()
                },
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
