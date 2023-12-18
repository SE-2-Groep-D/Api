namespace Api.Models.Domain.User;

public class Ervaringsdeskundige : Gebruiker {
  public string Postcode { get; set; }
  public bool ToestemmingBenadering { get; set; }
  public string Leeftijdscategorie { get; set; }

  public List<Voorkeurbenadering> Voorkeurbenaderingen { get; set; } = new();
  public List<Hulpmiddel> Hulpmiddelen { get; set; } = new();
  public List<TypeBeperking> TypeBeperkingen { get; set; } = new();

  public Guid? VoogdId { get; set; }
  public Voogd? Voogd { get; set; }

  public ICollection<Beschikbaarheid> Beschikbaarheden { get; } = new List<Beschikbaarheid>();


}
