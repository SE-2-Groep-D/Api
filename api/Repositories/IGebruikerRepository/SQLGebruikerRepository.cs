using Api.Data;
using Api.Models.Domain;
using Api.Models.Domain.User;
using Api.Models.DTO.Auth.response;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories.IGebruikerRepository;
public class SQLGebruikerRepository : IGebruikerRepository {

  private readonly AccessibilityDbContext _context;

  public SQLGebruikerRepository(AccessibilityDbContext context) {
    _context = context;
  }


  public Task<List<Gebruiker>> GetAllAsync() {
    return _context.Gebruikers.ToListAsync();
  }

  public async Task<RegisterResponseDto> VoegHulpmiddelenToe(string[] Hulpmiddelen, Guid GebruikerId) {
    var gebruiker = await _context.Ervaringsdeskundigen.FindAsync(GebruikerId);
    if (gebruiker == null) { return new RegisterResponseDto(false, "Gebruiker niet gevonden"); }

    foreach (var hulpmiddel in Hulpmiddelen) {
      var exists = _context.Hulpmiddelen.Any(h => h.Naam.Equals(hulpmiddel));

      if (!exists) {
        var nieuwHulpmiddel = new Hulpmiddel {
          Naam = hulpmiddel
        };
        await _context.Hulpmiddelen.AddAsync(nieuwHulpmiddel);
        _context.SaveChanges();
      }

      var test = _context.Hulpmiddelen.ToList();
      var hulp = await _context.Hulpmiddelen.FirstAsync(x => x.Naam.Equals(hulpmiddel));
      if (hulp == null) { return new RegisterResponseDto(false, "Een hulpmiddel kon niet worden gevonden"); }

      gebruiker.Hulpmiddelen.Add(hulp);
      hulp.Ervaringsdeskundigen.Add(gebruiker);
    }

    await _context.SaveChangesAsync();

    return new RegisterResponseDto(true, "Hulpmiddelen toegevoegd");
  }

}
