using Api.Models.Domain.Research;

namespace Api.Repositories.VragenRepository;
public interface IVraagRepository {

    
  Task<List<Vraag?>> GetAllAsync(Guid onderzoekId);
  Task<Vraag> GetByIdAsync(Guid id);

  Task<Vraag> CreateAsync(Vraag vraag);

    
  Task<Vraag?> UpdateAsync(Guid id, Vraag vraag);

  Task<bool> DeleteAsync(Guid id);

}