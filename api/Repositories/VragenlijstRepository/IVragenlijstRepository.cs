using Api.Models.Domain.Research;
using Api.Models.DTO.Onderzoek;

namespace Api.Repositories.VragenlijstRepository;
public interface IVragenlijstRepository {


  Task<List<Vragenlijst?>> GetAllAsync(Guid OnderzoekId);
  Task<VragenlijstDto?> GetByIdAsync(Guid id);

  Task<Vragenlijst> CreateAsync(Vragenlijst vragenlijst);


  Task<Vragenlijst?> UpdateAsync(Guid id, VragenlijstDto vragenlijst);

  Task<bool> DeleteAsync(Guid id);


}