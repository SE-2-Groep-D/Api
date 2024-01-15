using Api.Data;
using Api.Models.Domain.Research;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories.VragenRepository;
public class SQLVraagRepository : IVraagRepository {

  private readonly AccessibilityDbContext _context;

  public SQLVraagRepository(AccessibilityDbContext context) {
    _context = context;
  }

  public async Task<List<Vraag>> GetAllAsync(Guid VragenLijstId) {

    return await _context.Vragenlijsten.Where(v => v.Id == VragenLijstId).SelectMany(vl => vl.Vragen).ToListAsync();
    ;

  }

  public async Task<Vraag?> GetByIdAsync(Guid id) {

    return await _context.Vragen.FirstOrDefaultAsync(v => v.Id == id);

  }

  public async Task<Vraag> CreateAsync(Vraag vraag) {
    await _context.Vragen.AddAsync(vraag);
    await _context.SaveChangesAsync();
    return vraag;

  }

  public async Task<Vraag?> UpdateAsync(Guid id, Vraag vraag) {
    var bestaandVraag = await _context.Vragen.FindAsync(id);


    _context.Entry(bestaandVraag).CurrentValues.SetValues(vraag);
    await _context.SaveChangesAsync();
    return bestaandVraag;
  }

  public async Task<bool> DeleteAsync(Guid id) {

    var vragen = await _context.Vragen.FindAsync(id);
    if (vragen == null) {
      return false;
    }

    _context.Vragen.Remove(vragen);
    await _context.SaveChangesAsync();
    return true;

  }

}
