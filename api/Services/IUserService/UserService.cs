﻿using System.Text.RegularExpressions;
using Api.Data;
using Api.Models.Domain;
using Api.Models.Domain.User;
using Api.Models.DTO.Auth.response;
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
  private readonly AccessibilityDbContext _context;

  public UserService(UserManager<Gebruiker> gebruikerManager, IMapper mapper, AccessibilityDbContext context) {
    _gebruikerManager = gebruikerManager;

    _mapper = mapper;
    _context = context;
  }

  public async Task<RegisterResponseDto> Register(Gebruiker gebruiker, string? password, string[] roles) {
    if (!roles.Any()) {
      return new RegisterResponseDto(false, "Geef rol aan");
    }

    var identityResult = new IdentityResult();
    if (gebruiker.GoogleAccount && password == null) {
      identityResult = await _gebruikerManager.CreateAsync(gebruiker);
    }
    else if (password != null) {
      identityResult = await _gebruikerManager.CreateAsync(gebruiker, password);
    }
    else {
      return new RegisterResponseDto(false, "Er ging iets mis!");
    }


    if (!identityResult.Succeeded) {
      if (identityResult.Errors.Any(x => x.Code.Equals("DuplicateUserName"))) {
        return new RegisterResponseDto(false, "Er bestaat al een account met deze email");
      }

      return new RegisterResponseDto(false, "Er ging iets mis!");
    }

    identityResult = await _gebruikerManager.AddToRolesAsync(gebruiker, roles);
    if (!identityResult.Succeeded) {
      await _gebruikerManager.DeleteAsync(gebruiker);
      return new RegisterResponseDto(false, "Ongeldige rol");
    }

    var id = await _gebruikerManager.FindByEmailAsync(gebruiker.Email);


    return new RegisterResponseDto(true, "User was registerd! Please Login.") { Id = id.Id };
  }


  public async Task<Gebruiker?> GetUserByIdentification(string identification) {
    var emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
    var regex = new Regex(emailPattern);

    var user = regex.IsMatch(identification)
      ? await _gebruikerManager.FindByEmailAsync(identification)
      : await _gebruikerManager.FindByIdAsync(identification);

    return user;
  }


  public GebruikerDetails GetUserDetails(Gebruiker gebruiker) {
    var details = _mapper.Map<GebruikerDetails>(gebruiker);

    switch (gebruiker) {
      case Ervaringsdeskundige er:
        _context.Entry(er).Collection(h => h.Hulpmiddelen).Load();
        _context.Entry(er).Collection(v => v.Voorkeurbenaderingen).Load();
        details = _mapper.Map<ErvaringsDeskundigeDetails>(er);
        details.Type = "Ervaringsdeskundige";
        break;

      case Bedrijf er:
        details = _mapper.Map<BedrijfsDetails>(gebruiker);
        details.Type = "Bedrijf";
        break;

      case Medewerker er:
        details = _mapper.Map<MedewerkerDetails>(gebruiker);
        details.Type = "Medewerker";
        break;
    }

    return details;
  }

  public async Task<List<object>> GetUsersAsync() {
    var users = await _gebruikerManager.Users.ToListAsync();
    if (users.Count == 0) return new List<object>();
    var returned = new List<object>();

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

  public async Task<UpdateGebruikerResponse> UpdateUserProperties(Gebruiker gebruiker, InsertGebruikersInfoDto request,
    Dictionary<string, Action> properties) {
    await UpdateHulpMiddelen(gebruiker, request);
    var updatedGebruiker = await _context.Gebruikers.FirstOrDefaultAsync(g => g.Id.Equals(gebruiker.Id));
    var newUser = UpdateProperties(updatedGebruiker, request, properties); 
    var updated = await _gebruikerManager.UpdateAsync(newUser);
    if (!updated.Succeeded) return new UpdateGebruikerResponse(false, "Could not update user.");
    return new UpdateGebruikerResponse(true, "Succesfully updated the user.");
  }

  private async Task UpdateHulpMiddelen(Gebruiker gebruiker, InsertGebruikersInfoDto dto) {
    if (!(gebruiker is Ervaringsdeskundige) || dto.Hulpmiddelen == null) return;
    Ervaringsdeskundige? ervaringsdeskundige = await _context.Ervaringsdeskundigen
      .Include(e => e.Hulpmiddelen) // Include Hulpmiddelen to avoid lazy loading issues
      .FirstOrDefaultAsync(e => e.Id == gebruiker.Id);

    if (ervaringsdeskundige == null) {
      return;
    }
    
    ervaringsdeskundige.Hulpmiddelen = dto.Hulpmiddelen.Select(s => new Hulpmiddel {Naam = s}).ToList();
    await _context.SaveChangesAsync();
  }


  private Gebruiker UpdateProperties(Gebruiker gebruiker, object request, Dictionary<string, Action> properties) {
    var props = request.GetType().GetProperties();
    var userProps = gebruiker.GetType().GetProperties();

    foreach (var cp in props) {
      if (cp.GetValue(request) == null) continue;

      if (properties.TryGetValue(cp.Name, out var action)) {
        action();
        continue;
      }

      var userProp = userProps.FirstOrDefault(p => p.Name.Equals(cp.Name));
      if (userProp == null || cp.GetValue(request) == null) continue;
      var newValue = cp.GetValue(request);
      if (userProp.Name.Equals("Hulpmiddelen")) continue;

      userProp.SetValue(gebruiker, newValue);
    }

    return gebruiker;
  }

}
