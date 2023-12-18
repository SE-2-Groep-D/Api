
using Api.Models.Domain.User;
using API.Models.DTO.Gebruiker;
using API.Models.DTO.Gebruiker.request.UpdateGebruikerRequestDto;
using API.Models.DTO.Gebruiker.response.GebruikerDetailsResponseDto;

namespace Api.Services.IUserService;

public interface IUserService {
  public Task<string> Register(Gebruiker gebruiker, string password, string[] roles);
  //public Task<string> Register(Ervaringsdeskundige ervaringsdeskundige, string password, string[] roles);
  //public Task<string> Register(Bedrijf ervaringsdeskundige, string password, string[] roles);
  //public Task<string> Register(Ervaringsdeskundige ervaringsdeskundige, string password, string[] roles);

  public Task<Gebruiker?> GetUserByIdentification(string identification);
  public GebruikerDetailsResponseDto GetUserDetails(Gebruiker gebruiker);

  public Task<bool> UpdateUser(Gebruiker gebruiker, UpdateGebruikerRequestDto request);

}
