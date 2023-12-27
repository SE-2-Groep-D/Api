
using Api.Models.Domain.User;
using Api.Models.DTO.Auth;
using API.Models.DTO.Gebruiker.response.GebruikerDetailsResponseDto;
using API.Models.DTO.Gebruiker;
using Api.Models.DTO.Gebruiker.request;
using Api.Models.DTO.Gebruiker.response;


namespace Api.Services.IUserService;

public interface IUserService {
  public Task<string> Register(Gebruiker gebruiker, string password, string[] roles);
  //public Task<string> Register(Ervaringsdeskundige ervaringsdeskundige, string password, string[] roles);
  //public Task<string> Register(Bedrijf ervaringsdeskundige, string password, string[] roles);
  //public Task<string> Register(Ervaringsdeskundige ervaringsdeskundige, string password, string[] roles);
  public LoginResponseDto CreateLoginResponse(Gebruiker gebruiker, string jwtToken);
  public Task<Gebruiker?> GetUserByIdentification(string identification);
  public GebruikerDetails GetUserDetails(Gebruiker gebruiker);

  public Task<List<Object>> GetUsersAsync();

  public Task<UpdateGebruikerResponse> UpdateUser(Gebruiker gebruiker, InsertGebruikersInfoDto request);
  public Task<UpdateGebruikerResponse> UpdateUserProperties(Gebruiker gebruiker, InsertGebruikersInfoDto request, Dictionary<string, Action> properties);

}
