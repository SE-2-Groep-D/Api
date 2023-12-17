using Api.Data;
using Api.Models.Domain.Research;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories;

public class SQLOnderzoekRepository :IOnderzoekRepository {
  
  
  private AccessibilityDbContext _context;
  
  public SQLOnderzoekRepository(AccessibilityDbContext context) {
    this._context = context;
  }

  public async Task<IEnumerable<Onderzoek>> GetAllAsync(string status){
   
      return await _context.Onderzoeken.Where(o => o.Status == status).ToListAsync();
    
    
  }

  public async Task<Onderzoek> GetByIdAsync(Guid id) {

    return await _context.Onderzoeken.FirstOrDefaultAsync(o => o.Id == id);

  }

  public async Task<Onderzoek> CreateAsync(Onderzoek onderzoek) {
    _context.Add(onderzoek);
    await _context.SaveChangesAsync();
    return onderzoek;

  }

  public async Task<Onderzoek> UpdateAsync(Guid id, Onderzoek onderzoek) 
  {
    var bestaandOnderzoek = await _context.Onderzoeken.FindAsync(id);
    if (bestaandOnderzoek == null)
    {
      return null;
    }

    _context.Entry(bestaandOnderzoek).CurrentValues.SetValues(onderzoek);
    await _context.SaveChangesAsync();
    return bestaandOnderzoek;
  }


  public async Task<bool> DeleteAsync(Guid id) {

    var onderzoek = await _context.Onderzoeken.FindAsync(id);
    if (onderzoek == null)
    {
      return false;
    }

    _context.Onderzoeken.Remove(onderzoek);
    await _context.SaveChangesAsync();
    return true;

  }



}