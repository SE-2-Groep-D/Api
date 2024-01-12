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
  public string Leeftijdscategorie { get; set; }
    [Required]
  public string[]? NieuweHulpmiddelen { get; set; }

}
