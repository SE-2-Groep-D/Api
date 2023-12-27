namespace API.Models.DTO.Gebruiker.response.GebruikerDetailsResponseDto;

public class BedrijfsDetails : GebruikerDetails {

  public string Bedrijfsnaam { get; set; }
  public string Postcode { get; set; }
  public string Plaats { get; set; }
  public string Nummer { get; set; }
  public string WebsiteUrl { get; set; }
  public string Omschrijving { get; set; }
}