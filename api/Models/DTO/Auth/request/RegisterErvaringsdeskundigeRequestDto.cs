namespace Api.Models.DTO.Auth.request;
using Api.CustomActionFilters.CustomAttributes;
using System.ComponentModel.DataAnnotations;

public class RegisterErvaringsdeskundigeRequestDto : RegisterRequestDto {
  [Required]
  [Postcode]
  public string Postcode { get; set; }
  [Required]
  public bool ToestemmingBenadering { get; set; }
  [Required]
  [AllowedValues("0 tot 10", "10 tot 18", "18 tot 35", "35 tot 50", "50 tot 65", "65 of ouder")]
  public string Leeftijdscategorie { get; set; }
  public string[]? NieuweHulpmiddelen { get; set; }
  public string[]? NieuweVoorkeursbenaderingen { get; set; }

}
