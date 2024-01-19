using Api.CustomActionFilters.CustomAttributes;
using API.Models.DTO.Gebruiker.request.classes;
using System.ComponentModel.DataAnnotations;

namespace Api.Models.DTO.Gebruiker.request;
public class InsertGebruikersInfoDto {


  // Gebruikers info
  [EmailAddress]
  public string? Email { get; set; }
  public string? Voornaam { get; set; }
  public string? Achternaam { get; set; }
  public List<string>? Roles { get; set; }

  // Meerdere gebruikers

  [Phone]
  public string? PhoneNumber { get; set; }
  [Postcode]
  public string? Postcode { get; set; }


  // Ervaringsdeskundige
  public bool? ToestemmingBenadering { get; set; }
  [AllowedValues("0 tot 10", "10 tot 18", "18 tot 35", "35 tot 50", "50 tot 65", "65 of ouder", null)]
  public string? Leeftijdscategorie { get; set; }
  public List<string>? Benaderingen { get; set; }
  public List<string>? Hulpmiddelen { get; set; }
  public List<string>? TypeBeperkingen { get; set; }
  public VoogdDto? Voogd { get; set; }

  public ICollection<BeschikbaarheidDto>? Beschikbaarheden { get; set; }

  // Bedrijf
  public string? Bedrijfsnaam { get; set; }
  public string? Plaats { get; set; }
  public string? Straat { get; set; }
  public string? Nummer { get; set; }
  [Url]
  public string? WebsiteUrl { get; set; }
  public string? Omschrijving { get; set; }

  // Medewerker
  public string? Functie { get; set; }

}
