using System.Text.RegularExpressions;
using Api.Models.Domain.User;
using Api.Models.DTO.Auth;
using API.Models.DTO.Gebruiker;
using Api.Models.DTO.Gebruiker.request;
using Api.Models.DTO.Gebruiker.response;
using API.Models.DTO.Gebruiker.response.GebruikerDetailsResponseDto;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Api.Services.IUserService;
public class UserService : IUserService {

  private readonly UserManager<Gebruiker> _gebruikerManager;
  private readonly IMapper _mapper;

  public UserService(UserManager<Gebruiker> gebruikerManager, IMapper mapper ) {
    _gebruikerManager = gebruikerManager;
    _mapper = mapper;
  }

  public LoginResponseDto CreateLoginResponse (Gebruiker gebruiker, string jwtToken) {
  
    var response = new LoginResponseDto {
      UserId = gebruiker.Id,
      Voornaam = gebruiker.Voornaam,
      Achternaam = gebruiker.Achternaam,
      JwtToken = jwtToken
    };

    return response;
  }

  public async Task<string> Register(Gebruiker gebruiker, string password, string[] roles) {
    if (!roles.Any()) {
      return "Geef rol aan";
    }

    var identityResult = await _gebruikerManager.CreateAsync(gebruiker, password);
    if (!identityResult.Succeeded) {
      return "Er ging iets mis!";
    }

    identityResult = await _gebruikerManager.AddToRolesAsync(gebruiker, roles);
    if (!identityResult.Succeeded) {
      await _gebruikerManager.DeleteAsync(gebruiker);
      return "Ongeldige rol";
    }

    return "OK: User was registerd! Please Login.";
  }


  public async Task<Gebruiker?> GetUserByIdentification(string identification) {
    string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
    Regex regex = new Regex(emailPattern);

    Gebruiker? user = (regex.IsMatch(identification))
      ? await _gebruikerManager.FindByEmailAsync(identification)
      : await _gebruikerManager.FindByIdAsync(identification);

    return user;
  }


  public GebruikerDetails GetUserDetails(Gebruiker gebruiker) {
    if (gebruiker is Medewerker) {
      return _mapper.Map<MedewerkerDetails>(gebruiker);
    }

    if (gebruiker is Ervaringsdeskundige) {
      return _mapper.Map<ErvaringsDeskundigeDetails>(gebruiker);
    }

    if (gebruiker is Bedrijf) {
      return _mapper.Map<BedrijfsDetails>(gebruiker);
    }

    return _mapper.Map<GebruikerDetails>(gebruiker);
  }

  public async Task<List<Object>> GetUsersAsync() {
    var users = await _gebruikerManager.Users.ToListAsync();
    if (users.Count == 0) return new List<Object>();
    List<Object> returned = new List<Object>();

    foreach (var gebruiker in users) {
      var userDetails = GetUserDetails(gebruiker);
      returned.Add(userDetails);
    }

    return returned;
  }

  public async Task<UpdateGebruikerResponse> UpdateUser(Gebruiker user, InsertGebruikersInfoDto request) {
    var newUser = UpdateProperties(user, request, null);
    var updated = await _gebruikerManager.UpdateAsync(newUser);
    if (!updated.Succeeded) return new UpdateGebruikerResponse(false, "Could not update user.");
    return new UpdateGebruikerResponse(false, "Succesfully updated the user.");
  }

  public async Task<UpdateGebruikerResponse> UpdateUserProperties(Gebruiker gebruiker, InsertGebruikersInfoDto request, Dictionary<string, Action> properties) {
    var newUser = UpdateProperties(gebruiker, request, properties);
    var updated = await _gebruikerManager.UpdateAsync(newUser);
    if (!updated.Succeeded) return new UpdateGebruikerResponse(false, "Could not update user.");
    return new UpdateGebruikerResponse(true, "Succesfully updated the user.");
  }


  private Gebruiker UpdateProperties(Gebruiker gebruiker, Object request, Dictionary<string, Action> properties) {
    var props = request.GetType().GetProperties();
    var userProps = gebruiker.GetType().GetProperties();

    foreach (var cp in props) {
      if (cp.GetValue(request) == null) continue;

      if (properties.TryGetValue(cp.Name, out var action)) {
        action();
        continue;
      }

      var userProp = userProps.FirstOrDefault(p => p.Name.Equals(cp.Name));
      var newValue = cp.GetValue(request);
      if (userProp == null || cp.GetValue(request) == null) continue;
      userProp.SetValue(gebruiker, newValue);
    }

    return gebruiker;
  }


}
