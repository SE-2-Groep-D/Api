using Api.Models.Domain.Research;

namespace Api.Models.DTO.Onderzoek;
public class VraagDto {

  public Guid Id{ get; set; }
  public string Type{ get; set; }
  public string Onderwerp { get; set; }
  public Guid VragenlijstId { get; set; }
  public Vragenlijst Vragenlijst { get; set; } = null!;
  public ICollection<Antwoord> Antwoorden { get; } = new List<Antwoord>();

}