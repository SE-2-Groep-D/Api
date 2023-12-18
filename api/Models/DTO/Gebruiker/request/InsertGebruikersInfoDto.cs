using API.Models.DTO.Gebruiker.request.classes;

namespace Api.Models.DTO.Gebruiker.request; 
public class InsertGebruikersInfoDto {

    
  // Gebruikers info
  public string? Email { get; set; }
  public string? Voornaam { get; set; }
  public string? Achternaam { get; set; }
  public List<string>? Roles { get; set; }
 
  // Meerdere gebruikers
  
  public string? PhoneNumber { get; set; }
  public string? Postcode { get; set; }
  
  
  // Ervaringsdeskundige
  public bool? ToestemmingBenadering { get; set; }
  public string? Leeftijdscategorie { get; set; }
  public List<string>? Benaderingen { get; set; }
  public List<string>? Hulpmiddelen { get; set; }
  public List<string>? TypeBeperkingen { get; set; }
  public Voogd? Voogd { get; set; }
  
  public ICollection<Beschikbaarheid>? Beschikbaarheden { get; set; }
  
  // Bedrijf
  public string? Bedrijfsnaam { get; set; }
  public string? Plaats { get; set; }
  public string? WebsiteUrl { get; set; }
  public string? Omschrijving { get; set; }
  
  // Medewerker
  public string? Functie { get; set; }


  

}
