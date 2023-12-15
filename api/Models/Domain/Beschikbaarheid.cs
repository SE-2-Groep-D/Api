using Api.Models.Domain.User;

namespace Api.Models.Domain;
public class Beschikbaarheid {

  public Guid Id { get; set; }
  public DateTime BeginDatumTijd { get; set; }
  public DateTime EindDatumTijd { get; set; }

  public Guid ErvaringsdeskundigeId { get; set; }
  public Ervaringsdeskundige Ervaringsdeskundige { get; set; } = null!;

}
