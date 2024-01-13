using Api.Data;
using Api.Models.Domain;
using Api.Models.Domain.Research;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories;
public class SQLOnderzoekRepository : IOnderzoekRepository {


  private readonly AccessibilityDbContext _context;

  public SQLOnderzoekRepository(AccessibilityDbContext context) {
    _context = context;
  }

  public async Task<List<Onderzoek>> GetAllAsync(string? status) {
    return status == null
      ? await _context.Onderzoeken.ToListAsync()
      : await _context.Onderzoeken.Where(o => o.Status.ToString() == status).ToListAsync();
  }

  public async Task<Onderzoek?> GetByIdAsync(Guid id) {
    return await _context.Onderzoeken.FirstOrDefaultAsync(o => o.Id == id);
  }

  public async Task<Onderzoek> CreateAsync(Onderzoek onderzoek) {
    await _context.AddAsync(onderzoek);
    await _context.SaveChangesAsync();
    return onderzoek;

  }

  public async Task<Onderzoek?> UpdateAsync(Guid id, Onderzoek onderzoek) {
    var bestaandOnderzoek = await _context.Onderzoeken.FindAsync(id);


    _context.Entry(bestaandOnderzoek).CurrentValues.SetValues(onderzoek);
    await _context.SaveChangesAsync();
    return bestaandOnderzoek;
  }


  public async Task<bool> DeleteAsync(Guid id) {

    var onderzoek = await _context.Onderzoeken.FindAsync(id);
    if (onderzoek == null) {
      return false;
    }

    _context.Onderzoeken.Remove(onderzoek);
    await _context.SaveChangesAsync();
    return true;

  }
  
  public async Task<OnderzoekErvaringsdekundige> CreateRegistrationAsync(OnderzoekErvaringsdekundige registration) {
    await _context.AddAsync(registration);
    await _context.SaveChangesAsync();
    return registration;

  }
  
  
  public async Task<List<OnderzoekErvaringsdekundige>> GetRegistrationByResearchId(Guid id) {
    return await _context.OnderzoekErvaringsdekundigen
      .Where(o => o.OnderzoekId == id)
      .ToListAsync();
  }


}
