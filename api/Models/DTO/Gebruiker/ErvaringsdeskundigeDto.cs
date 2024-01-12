using Api.Models.Domain;

namespace API.Models.DTO.Gebruiker;
public class ErvaringsDeskundigeDetails : GebruikerDetails {

  public string Postcode { get; set; }
  public bool ToestemmingBenadering { get; set; }
  public string Leeftijdscategorie { get; set; }

  public List<Voorkeurbenadering> Voorkeurbenaderingen { get; } = new();
  public List<Hulpmiddel> Hulpmiddelen { get; } = new();
  public List<TypeBeperking> TypeBeperkingen { get; } = new();

  public Guid? VoogdId { get; set; }
  public Voogd? Voogd { get; set; }

  public ICollection<Beschikbaarheid> Beschikbaarheden { get; } = new List<Beschikbaarheid>();

}
