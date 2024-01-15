using Microsoft.AspNetCore.Identity;

namespace Api.Models.Domain.User;
using Api.Models.Domain.Bericht;

public class Gebruiker : IdentityUser<Guid> {

  public string Voornaam { get; set; }
  public string Achternaam { get; set; }
  public bool GoogleAccount { get; set; } = false;
  

  public ICollection<Bericht> VerzondenBerichten { get; } = new List<Bericht>();
  public ICollection<Bericht> OntvangenBerichten { get; } = new List<Bericht>();
}
