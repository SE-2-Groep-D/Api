using Api.Models.Domain.Research;

namespace Api.Repositories.VragenlijstRepository;
public interface IVragenlijstRepository {
  
  
  Task<List<Vragenlijst?>> GetAllAsync(Guid OnderzoekId);
  Task<Vragenlijst> GetByIdAsync(Guid id);

  Task<Vragenlijst> CreateAsync(Vragenlijst vragenlijst);

    
  Task<Vragenlijst?> UpdateAsync(Guid id, Vragenlijst vragenlijst);

  Task<bool> DeleteAsync(Guid id);
  

}