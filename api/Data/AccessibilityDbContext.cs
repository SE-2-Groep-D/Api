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


            var readerRoleId = "5d67df1b-5426-4158-aa57-7bc2d1030980";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name = "Beheerder",
                    NormalizedName = "Beheerder".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
