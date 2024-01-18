using System.ComponentModel.DataAnnotations;

namespace API.Models.DTO.Gebruiker;
public class GebruikerDetails {

  public Guid Id { get; set; }

  public string Voornaam { get; set; }
  public string Achternaam { get; set; }
  [EmailAddress]
  public string Email { get; set; }
  public string Type { get; set; }

}

