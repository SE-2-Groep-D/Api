using Microsoft.AspNetCore.Identity;

namespace Api.Models.Domain.User;

public class Gebruiker : IdentityUser<Guid> {
  public string Voornaam { get; set; }
  public string Achternaam { get; set; }
  public bool GoogleAccount { get; set; } = false;
}

