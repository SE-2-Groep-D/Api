namespace Api.Models.Domain.Research; 
public class Antwoord {

  public Guid Id { get; set; }
  public string Tekst { get; set; }
  public Guid VraagtId { get; set; }
  public Vraag Vraag { get; set; }
}