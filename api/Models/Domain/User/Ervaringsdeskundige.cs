using Api.Models.Domain.Research;

namespace Api.Models.Domain.User;

public class Ervaringsdeskundige : Gebruiker {
  public string Postcode { get; set; }
  public bool ToestemmingBenadering { get; set; }
  public string Leeftijdscategorie { get; set; }

  public List<Voorkeurbenadering> Voorkeurbenaderingen { get; } = new();
  public List<Hulpmiddel> Hulpmiddelen { get; } = new();
  public List<TypeBeperking> TypeBeperkingen { get; } = new();

  public Guid? VoogdId { get; set; }
  public Voogd? Voogd { get; set; }

  public ICollection<Beschikbaarheid> Beschikbaarheden { get; } = new List<Beschikbaarheid>();
  public List<Onderzoek> Onderzoeken { get; } = new();
  public List<OnderzoekErvaringsdekundige> OnderzoekErvaringsdekundigen { get; } = new();


}
