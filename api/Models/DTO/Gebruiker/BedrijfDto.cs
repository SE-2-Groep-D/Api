using Api.CustomActionFilters.CustomAttributes;
using System.ComponentModel.DataAnnotations;

namespace API.Models.DTO.Gebruiker.response.GebruikerDetailsResponseDto;
public class BedrijfsDetails : GebruikerDetails {

  public string Bedrijfsnaam { get; set; }
  [Postcode]
  public string Postcode { get; set; }
  public string Plaats { get; set; }
  public string Straat { get; set; }
  public string Nummer { get; set; }
  [Url]
  public string WebsiteUrl { get; set; }
  public string Omschrijving { get; set; }

}
