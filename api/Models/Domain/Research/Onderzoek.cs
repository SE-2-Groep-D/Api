using System.ComponentModel.DataAnnotations;
using Api.Models.Domain.User;

namespace Api.Models.Domain.Research; 

public class Onderzoek {

  public Guid Id { get; set; }
  //duur
  public DateTime StartDatum { get; set; }
  public string Omschrijving  { get; set; }
  public double Vergoeding { get; set; }
  public string Locatie { get; set; }
  public string Status { get; set; }

  public ICollection<Vragenlijst> Vragenlijst { get; } = new List<Vragenlijst>();
  //resultaat
  public List<Ervaringsdeskundige> Ervaringsdeskundigen { get; } = new();
  public List<OnderzoekErvaringsdekundige> OnderzoekErvaringsdekundigen { get; } = new();
  public Guid BedrijfId { get; set; }
  public Bedrijf Bedrijf { get; set; } = null!;
  




}
