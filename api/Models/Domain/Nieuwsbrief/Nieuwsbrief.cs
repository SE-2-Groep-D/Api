using System.ComponentModel.DataAnnotations.Schema;
using Api.Models.Domain.User;

namespace Api.Models.Domain.News; 
public class Nieuwsbrief {

  public Guid Id { get; set; }
  public DateTime Datum { get; set; }
  public string Titel { get; set; }
  public string Inhoud { get; set; }
  
  public Medewerker Medewerker { get; set; }
  
  [ForeignKey("Medewerker")]
  public Guid MedewerkerId { get; set; }
  
}
