using Api.Models.Domain;

namespace API.Models.DTO.Gebruiker.request.UpdateGebruikerRequestDto;

public class UpdateGebruikerRequestDto {
  
  public string? Email { get; set; }
  public string? Voornaam { get; set; }
  public string? Achternaam { get; set; }
  public string? Roles { get; set; }
  
  public string? PhoneNumber { get; set; }
  
}