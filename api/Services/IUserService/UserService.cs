using System.Text.RegularExpressions;
using Api.Models.Domain.User;
using API.Models.DTO.Gebruiker;
using API.Models.DTO.Gebruiker.request.UpdateGebruikerRequestDto;
using API.Models.DTO.Gebruiker.response.GebruikerDetailsResponseDto;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using BedrijfDto = API.Models.DTO.Gebruiker.response.GebruikerDetailsResponseDto.BedrijfDto;
using ErvaringsdeskundigeDto = API.Models.DTO.Gebruiker.response.GebruikerDetailsResponseDto.ErvaringsdeskundigeDto;
using MedewerkerDto = API.Models.DTO.Gebruiker.response.GebruikerDetailsResponseDto.MedewerkerDto;

namespace Api.Services.IUserService;
public class UserService : IUserService {

  private readonly UserManager<Gebruiker> _gebruikerManager;
  private readonly IMapper _mapper;

  public UserService(UserManager<Gebruiker> gebruikerManager, IMapper mapper) {
    _gebruikerManager = gebruikerManager;
    _mapper = mapper;
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

  public async Task<bool> UpdateUser(Gebruiker user, UpdateGebruikerRequestDto request) {
    var dtoProperties = request.GetType().GetProperties();
    var userProperties = user.GetType().GetProperties();
    
    
    foreach (var dtoProp in dtoProperties) {
      var userProp = userProperties.FirstOrDefault(p => p.Name == dtoProp.Name);
      if (userProp == null) continue;
      var dtoValue = dtoProp.GetValue(request);
      if(dtoValue == null) continue;
      userProp.SetValue(user, dtoValue);
    }

    if (user is Ervaringsdeskundige ervaringsdeskundige && request is UpdateErvaringsdeskundigeDto updateDto) {
      await UpdateErvaringsDeskundige(ervaringsdeskundige, updateDto);
    }
    
    
    var result = await _gebruikerManager.UpdateAsync(user);
    if (!result.Succeeded) return false;
    return true;
  }

  private async Task<bool> UpdateErvaringsDeskundige(Ervaringsdeskundige ervaringsdeskundige, UpdateErvaringsdeskundigeDto dto) {
    return false;
  }

}
