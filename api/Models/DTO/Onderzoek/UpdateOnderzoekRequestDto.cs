namespace Api.Models.DTO.Onderzoek;
public class UpdateOnderzoekRequestDto {


  //duur
  public DateTime StartDatum { get; set; }
  public string? Omschrijving  { get; set; }
  public double? Vergoeding { get; set; }
  public string? Locatie { get; set; }
  public string? Status { get; set; }


}