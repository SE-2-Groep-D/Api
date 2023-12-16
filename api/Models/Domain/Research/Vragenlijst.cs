namespace Api.Models.Domain.Research; 
public class Vragenlijst {

  public Guid Id { get; set; }
  public string Titel { get; set; }
  public string Samenvatting { get; set; }

  public Guid OnderzoekId { get; set; }
  public Onderzoek Onderzoek { get; set; }
  public ICollection<Vraag> Vragen { get; } = new List<Vraag>();


}