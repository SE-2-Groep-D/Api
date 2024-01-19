using Api.Models.Domain.User;
using Api.Models.DTO.Auth.response;
using API.Models.DTO.Gebruiker;
using Api.Models.DTO.Gebruiker.request;
using Api.Models.DTO.Gebruiker.response;

namespace Api.Services.IUserService;
public interface IUserService {

  public Task<RegisterResponseDto> Register(Gebruiker gebruiker, string password, string[] roles);
  public Task<Gebruiker?> GetUserByIdentification(string identification);
  public GebruikerDetails GetUserDetails(Gebruiker gebruiker);
  public Task<List<object>> GetUsersAsync();

  public Task<UpdateGebruikerResponse> UpdateUser(Gebruiker gebruiker, InsertGebruikersInfoDto request);

  public Task<UpdateGebruikerResponse>
    UpdateUserProperties(Gebruiker gebruiker, InsertGebruikersInfoDto request, Dictionary<string, Action> properties);

}
