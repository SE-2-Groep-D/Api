using Api.Models.Domain.Research;

namespace Api.Repositories {
  public interface IOnderzoekRepository {

    Task<IEnumerable<Onderzoek>> GetAllAsync(string status);
    Task<Onderzoek> GetByIdAsync(Guid id);

    Task<Onderzoek> CreateAsync(Onderzoek onderzoek);

    
    Task<Onderzoek> UpdateAsync(Guid id, Onderzoek onderzoek);

    Task<bool> DeleteAsync(Guid id);


  }
}
