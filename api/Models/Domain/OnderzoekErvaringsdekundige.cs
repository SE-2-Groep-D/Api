using Api.Models.Domain.Research;
using Api.Models.Domain.User;

namespace Api.Models.Domain;
public class OnderzoekErvaringsdekundige {

  public Guid OnderzoekId { get; set; }
  public Guid ErvaringsdeskundigeId { get; set; }
  public DateTime datum { get; set; }

  public Onderzoek Onderzoek { get; set; }
  public Ervaringsdeskundige Ervaringsdeskundige { get; set; }

}
