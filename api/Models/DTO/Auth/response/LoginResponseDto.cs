namespace Api.Models.DTO.Auth.response; 
public class LoginResponseDto {

  public Guid Id { get; set; }
  public string Voornaam { get; set; }
  public string Achternaam { get; set; }
  public string? UserType { get; set; }

}
