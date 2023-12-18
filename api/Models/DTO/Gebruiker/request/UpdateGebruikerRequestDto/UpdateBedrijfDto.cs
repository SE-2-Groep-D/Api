namespace API.Models.DTO.Gebruiker.request.UpdateGebruikerRequestDto;

public class UpdateBedrijfDto : UpdateGebruikerRequestDto {
  
  public string? Naam { get; set; }
  public string? Postcode { get; set; }
  public string? Plaats { get; set; }
  public string? WebsiteUrl { get; set; }
  public string? Omschrijving { get; set; }
}