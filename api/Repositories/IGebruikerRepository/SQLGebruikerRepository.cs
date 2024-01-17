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
        await _context.SaveChangesAsync();
      }

      //var test = _context.Hulpmiddelen.ToList();
      var hulp = await _context.Hulpmiddelen.FirstAsync(x => x.Naam.Equals(hulpmiddel));
      if (hulp == null) { return new RegisterResponseDto(false, "Een hulpmiddel kon niet worden gevonden"); }

      gebruiker.Hulpmiddelen.Add(hulp);
      hulp.Ervaringsdeskundigen.Add(gebruiker);
    }

    await _context.SaveChangesAsync();

    return new RegisterResponseDto(true, "Hulpmiddelen toegevoegd");
  }

  public async Task<RegisterResponseDto> VoegBenaderingToe(string[] Benaderingen, Guid GebruikerId) {
    var gebruiker = await _context.Ervaringsdeskundigen.FindAsync(GebruikerId);
    if (gebruiker == null) { return new RegisterResponseDto(false, "Gebruiker niet gevonden"); }

    foreach (var benadering in Benaderingen) {
      var exists = _context.Voorkeurbenaderingen.Any(h => h.Type.Equals(benadering));

      if (!exists) {
        var nieuwBenadering = new Voorkeurbenadering {
          Type = benadering
        };
        await _context.Voorkeurbenaderingen.AddAsync(nieuwBenadering);
        await _context.SaveChangesAsync();
      }

      //var test = _context.Hulpmiddelen.ToList();
      var ben = await _context.Voorkeurbenaderingen.FirstAsync(x => x.Type.Equals(benadering));
      if (ben == null) { return new RegisterResponseDto(false, "Een voorkeurbenadering kon niet worden gevonden"); }

      gebruiker.Voorkeurbenaderingen.Add(ben);
      ben.Ervaringsdeskundigen.Add(gebruiker);
    }

    await _context.SaveChangesAsync();

    return new RegisterResponseDto(true, "Benaderingen toegevoegd");
  }

}