using API.Models.DTO.Gebruiker.request.UpdateGebruikerRequestDto.classes;

namespace API.Models.DTO.Gebruiker.request.UpdateGebruikerRequestDto;

public class UpdateErvaringsdeskundigeDto : UpdateGebruikerRequestDto {
  
  public string? Postcode { get; set; }
  public bool? ToestemmingBenadering { get; set; }
  public string? Leeftijdscategorie { get; set; }

  public List<string> benaderingen { get; set; }
  public List<string>? Hulpmiddelen { get; set; }
  public List<string>? TypeBeperkingen { get; set; }
  
  public Voogd? Voogd { get; set; }

  public ICollection<Beschikbaarheid>? Beschikbaarheden { get; } = new List<Beschikbaarheid>();

}

