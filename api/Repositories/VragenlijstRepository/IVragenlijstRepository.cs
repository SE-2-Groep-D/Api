using Api.Models.Domain.Research;
using Api.Models.DTO.Onderzoek;

namespace Api.Repositories.VragenlijstRepository;
public interface IVragenlijstRepository {


  Task<List<Questionlist?>> GetAllAsync(Guid OnderzoekId);
  Task<Questionlist?> GetByIdAsync(Guid id);

  Task<Questionlist> CreateAsync(Questionlist vragenlijst);


 Task<Questionlist?> UpdateAsync(Guid id, Questionlist vragenlijst);

  Task<bool> DeleteAsync(Guid id);


}