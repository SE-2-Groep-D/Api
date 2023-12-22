using System.Text.RegularExpressions;
using Api.Models.Domain.User;
using Api.Models.DTO.Auth;
using API.Models.DTO.Gebruiker.response.GebruikerDetailsResponseDto;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

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


  public GebruikerDetailsResponseDto GetUserDetails(Gebruiker gebruiker) {
    if (gebruiker is Medewerker) {
      return _mapper.Map<MedewerkerDto>(gebruiker);
    }

    if (gebruiker is Ervaringsdeskundige) {
      return _mapper.Map<ErvaringsdeskundigeDto>(gebruiker);
    }

    if (gebruiker is Bedrijf) {
      return _mapper.Map<BedrijfDto>(gebruiker);
    }

    return _mapper.Map<GebruikerDetailsResponseDto>(gebruiker);
  }

}
