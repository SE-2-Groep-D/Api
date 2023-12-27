namespace Api.Models.Domain.Research;
public class Vraag {

  public Guid Id { get; set; }
  public string Type { get; set; }
  public string Onderwerp { get; set; }
  public Guid VragenlijstId { get; set; }
  public Vragenlijst Vragenlijst { get; set; } = null!;
  public ICollection<Antwoord> Antwoorden { get; } = new List<Antwoord>();

}