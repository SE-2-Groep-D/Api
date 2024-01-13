using Api.Models.Domain.User;
using Api.Models.DTO.Auth.response;

namespace Api.Repositories.IGebruikerRepository;
public interface IGebruikerRepository {

  public Task<List<Gebruiker>> GetAllAsync();
  public Task<RegisterResponseDto> VoegHulpmiddelenToe(string[] Hulpmiddelen, Guid GebruikerId);

}
