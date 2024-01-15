using Api.Models.Domain.User;

namespace Api.Models.Domain;
public class Voorkeurbenadering {

  public Guid Id { get; set; }
  public string Type { get; set; }
  public List<Ervaringsdeskundige> Ervaringsdeskundigen { get; } = new();

}
