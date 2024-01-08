namespace Api.Models.DTO.Onderzoek;
public class UpdateOnderzoekRequestDto {


  //duur
  
  public string? Titel { get; set; }

  public string? AantalParticipanten { get; set; }


  public string? websiteUrl { get; set; }
  public DateTime? StartDatum { get; set; }
  public string? Omschrijving { get; set; }
  public double? Vergoeding { get; set; }
  public string? Locatie { get; set; }
  public string? Type { get; set; }
  public string? Status { get; set; }


}