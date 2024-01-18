using Api.Data;
using Api.Models.Domain.Bericht;
using Api.Models.DTO.Bericht;
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


    public async Task<IEnumerable<Bericht>> GetBerichten(Guid verzenderId, Guid ontvangerId) {
      return await _context.Berichten
          .Where(b => (b.VerzenderId == verzenderId && b.OntvangerId == ontvangerId) ||
                      (b.VerzenderId == ontvangerId && b.OntvangerId == verzenderId))
          .OrderBy(b => b.DatumTijd)
          .ToListAsync();
    }

  }
}
