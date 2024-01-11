using Api.Data;
using Api.Models.Domain;
using Api.Models.Domain.User;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories.IGebruikerRepository;
public class SQLGebruikerRepository : IGebruikerRepository {

  private AccessibilityDbContext _context;

  public SQLGebruikerRepository(AccessibilityDbContext context) {
    this._context = context;
  }


  public Task<List<Gebruiker>> GetAllAsync() {
    return _context.Gebruikers.ToListAsync();
  }

}
