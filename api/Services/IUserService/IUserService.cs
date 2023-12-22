
using Api.Models.Domain.User;
using Api.Models.DTO.Auth;
using API.Models.DTO.Gebruiker.response.GebruikerDetailsResponseDto;

namespace Api.Services.IUserService;

public interface IUserService {
  public Task<string> Register(Gebruiker gebruiker, string password, string[] roles);
  //public Task<string> Register(Ervaringsdeskundige ervaringsdeskundige, string password, string[] roles);
  //public Task<string> Register(Bedrijf ervaringsdeskundige, string password, string[] roles);
  //public Task<string> Register(Ervaringsdeskundige ervaringsdeskundige, string password, string[] roles);
  public LoginResponseDto CreateLoginResponse(Gebruiker gebruiker, string jwtToken);
  public Task<Gebruiker?> GetUserByIdentification(string identification);
  public GebruikerDetailsResponseDto GetUserDetails(Gebruiker gebruiker);
}
