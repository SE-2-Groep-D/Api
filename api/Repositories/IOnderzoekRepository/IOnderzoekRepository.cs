using Api.Models.Domain;
using Api.Models.Domain.Research;
using Api.Models.DTO.Onderzoek;

namespace Api.Repositories; 
public interface IOnderzoekRepository {

  Task<List<Onderzoek?>> GetAllAsync(string? status);
  Task<Onderzoek?> GetByIdAsync(Guid id);

  Task<Onderzoek?> CreateAsync(Onderzoek onderzoek);


  Task<Onderzoek?> UpdateAsync(Guid id, Onderzoek onderzoek);

  Task<bool> DeleteAsync(Guid id);
  
  Task<OnderzoekErvaringsdekundige?> CreateRegistrationAsync(OnderzoekErvaringsdekundige registration);
  
  Task<OnderzoekErvaringsdekundige?> GetRegistrationByResearchId(Guid id);

}
