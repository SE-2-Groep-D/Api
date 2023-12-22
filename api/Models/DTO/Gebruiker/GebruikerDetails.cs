namespace API.Models.DTO.Gebruiker;

public class GebruikerDetails {

  public Guid Id { get; set; }
  
  public string Voornaam { get; set; }
  public string Achternaam { get; set; }
  public string Email { get; set; }
}