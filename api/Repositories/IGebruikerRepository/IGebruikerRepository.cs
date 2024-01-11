using Api.Models.Domain.User;

namespace Api.Repositories.IGebruikerRepository;

public interface IGebruikerRepository {
  public Task<List<Gebruiker>> GetAllAsync();
}
