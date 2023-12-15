namespace Api.Models.Domain.User {
  public class Bedrijf : Gebruiker {
    public string NaamBedrijf { get; set; }
    public string Postcode { get; set; }
    public string Plaats { get; set; }
    public string Nummer { get; set; }
    public string WebsiteUrl { get; set; }
    public string Omschrijving { get; set; }

  }
}
