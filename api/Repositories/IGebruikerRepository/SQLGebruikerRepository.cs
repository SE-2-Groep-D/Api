using Api.Data;
using Api.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories; 

public class SQLGebruikerRepository : IGebruikerRepository {

    private AccessibilityDbContext _context;
    
    public SQLGebruikerRepository(AccessibilityDbContext context) {
        this._context = context;
    }


    public Task<List<Gebruiker>> GetAllAsync() {       
        return _context.Gebruikers.ToListAsync();
    }
}