namespace Api.Models.DTO.Onderzoek;
public class AddOnderzoekRequestDto {

  //duur
  public string Titel { get; set; }
  public int AantalParticipanten { get; set; }
  public string WebsiteUrl { get; set; }
  public DateTime StartDatum { get; set; }
  public string Omschrijving { get; set; }
  public double Vergoeding { get; set; }
  public string Locatie { get; set; }
  public string Status { get; set; }
  public Guid BedrijfId { get; set; }
  public string Type { get; set; }

}