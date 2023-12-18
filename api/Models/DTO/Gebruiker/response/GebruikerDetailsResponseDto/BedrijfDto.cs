namespace API.Models.DTO.Gebruiker.response.GebruikerDetailsResponseDto;

public class BedrijfDto : GebruikerDetailsResponseDto {
  
  public string NaamBedrijf { get; set; }
  public string Postcode { get; set; }
  public string Plaats { get; set; }
  public string Nummer { get; set; }
  public string WebsiteUrl { get; set; }
  public string Omschrijving { get; set; }
}