using Api.Data;
using Api.Models.Domain.Research;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories.VragenlijstRepository;
public class SQLVragenlijstRepository : IVragenlijstRepository {

  private AccessibilityDbContext _context;
  
  public SQLVragenlijstRepository(AccessibilityDbContext context) {
    _context = context;
  }
  
  public async Task<List<Vragenlijst>> GetAllAsync(Guid OnderzoekId) {

    return await _context.Vragenlijsten.Where(v => v.OnderzoekId == OnderzoekId).ToListAsync();
    
  }
  
  public async Task<Vragenlijst?> GetByIdAsync(Guid id) {

    return await _context.Vragenlijsten.FirstOrDefaultAsync(v => v.Id == id);

  }
  
  public async Task<Vragenlijst> CreateAsync(Vragenlijst vragenlijst) {
    await _context.Vragenlijsten.AddAsync(vragenlijst);
    await _context.SaveChangesAsync();
    return vragenlijst;

  }
  
  public async Task<Vragenlijst?> UpdateAsync(Guid id, Vragenlijst vragenlijst) 
  {
    var bestaandVragenlijst = await _context.Vragenlijsten.FindAsync(id);


    _context.Entry(bestaandVragenlijst).CurrentValues.SetValues(vragenlijst);
    await _context.SaveChangesAsync();
    return bestaandVragenlijst;
  }
  
  public async Task<bool> DeleteAsync(Guid id) {

    var vragenlijst = await _context.Vragenlijsten.FindAsync(id);
    if (vragenlijst == null)
    {
      return false;
    }

    _context.Vragenlijsten.Remove(vragenlijst);
    await _context.SaveChangesAsync();
    return true;

  }



}