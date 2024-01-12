using Api.Models.Domain.Research;

namespace Api.Repositories.AntwoordRepository;
public interface IAntwoordRepository {

  Task<List<Antwoord?>> GetAllAsync(Guid vraagId);
  Task<Antwoord> GetByIdAsync(Guid id);

  Task<Antwoord> CreateAsync(Antwoord antwoord);


  Task<Antwoord?> UpdateAsync(Guid id, Antwoord antwoord);

  Task<bool> DeleteAsync(Guid id);

}
