namespace Api.Models.DTO.Auth.request {
  public class RegisterErvaringsdeskundigeRequestDto : RegisterRequestDto {
    public string Postcode { get; set; }
    public bool ToestemmingBenadering { get; set; }
    public string Leeftijdscategorie { get; set; }
    public string[]? Hulpmiddelen { get; set; }

  }
}
