using Api.Data;
using Api.Models.Domain.Bericht;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories.IBerichtRepository {
  public class SQLBerichtRepository : IBerichtRepository {

    private AccessibilityDbContext _context;

    public SQLBerichtRepository(AccessibilityDbContext context) {
      this._context = context;
    }

    public async Task<Bericht> CreateBericht(Bericht bericht) {

      await _context.Berichten.AddAsync(bericht);
      await _context.SaveChangesAsync();
      return bericht;

    }

    public async Task<IEnumerable<Bericht>> GetChatsByUserId(Guid userId) {
      return await _context.Berichten
          .Where(b => b.VerzenderId == userId || b.OntvangerId == userId)
          .ToListAsync();
    }

  }
}
