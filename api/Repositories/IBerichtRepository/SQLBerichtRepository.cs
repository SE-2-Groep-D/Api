using Api.Data;
using Api.Models.Domain.Bericht;

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
  }
}
