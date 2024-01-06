using Api.Data;
using Api.Models.Domain.Research;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories.AntwoordRepository;


public class SQLAntwoordRepository : IAntwoordRepository {
  
    
  private AccessibilityDbContext _context;
  
  public SQLAntwoordRepository(AccessibilityDbContext context) {
    _context = context;
  }
  

 /* public async  Task<List<Antwoord?>> GetAllAsync(Guid vraagId) {
    
  //  return await _context.Vragen.Where(v => v.Id == vraagId).SelectMany(vl => vl.Antwoorden).ToListAsync();
  }*/

  public async Task<Antwoord> GetByIdAsync(Guid id) {
    
    return await _context.Antwoorden.FirstOrDefaultAsync(v => v.Id == id);
    
  }

  public async Task<Antwoord> CreateAsync(Antwoord antwoord) {
    
    await _context.Antwoorden.AddAsync(antwoord);
    await _context.SaveChangesAsync();
    return antwoord;
  }


  public async Task<Antwoord?> UpdateAsync(Guid id, Antwoord antwoord){
    var bestaandAntwoord = await _context.Antwoorden.FindAsync(id);


    _context.Entry(bestaandAntwoord).CurrentValues.SetValues(antwoord);
    await _context.SaveChangesAsync();
    return bestaandAntwoord;
  }

  public async Task<bool> DeleteAsync(Guid id) {

    var antwoord = await _context.Antwoorden.FindAsync(id);
    if (antwoord == null)
    {
      return false;
    }

    _context.Antwoorden.Remove(antwoord);
    await _context.SaveChangesAsync();
    return true;

  }
  
  
}