using Api.CustomActionFilters.CustomAttributes;
using Api.Models.Domain;
using Api.Models.DTO.Gebruiker;
using System.ComponentModel.DataAnnotations;

namespace API.Models.DTO.Gebruiker;
public class ErvaringsDeskundigeDetails : GebruikerDetails {

  [Postcode]
  public string Postcode { get; set; }
  public bool ToestemmingBenadering { get; set; }
  [AllowedValues("0 tot 10", "10 tot 18", "18 tot 35", "35 tot 50", "50 tot 65", "65 of ouder")]
  public string Leeftijdscategorie { get; set; }

  public List<VoorkeurbenaderingDto> Voorkeurbenaderingen { get; } = new();
  public List<HulpmiddelDto> Hulpmiddelen { get; } = new();
  public List<TypeBeperking> TypeBeperkingen { get; } = new();

  public Guid? VoogdId { get; set; }
  public Voogd? Voogd { get; set; }

  public ICollection<Beschikbaarheid> Beschikbaarheden { get; } = new List<Beschikbaarheid>();

}
