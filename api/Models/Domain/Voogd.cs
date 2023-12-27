using Api.Models.Domain.User;

namespace Api.Models.Domain;
public class Voogd {

  public Guid Id { get; set; }
  public string Name { get; set; }
  public string Email { get; set; }
  public string Phonenumber { get; set; }
  public List<Ervaringsdeskundige> Ervaringsdeskundigen { get; } = new();

}
