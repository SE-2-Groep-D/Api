namespace Api.Models.Domain.User;
public class Bedrijf : Gebruiker {

  public string Bedrijfsnaam { get; set; }
  public string Postcode { get; set; }
  public string Plaats { get; set; }
  public string Straat { get; set; }
  public string Nummer { get; set; }
  public string PhoneNumber { get; set; }
  public string WebsiteUrl { get; set; }
  public string Omschrijving { get; set; }

}
