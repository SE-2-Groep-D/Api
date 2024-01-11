namespace Api.Models.DTO.Auth {
  public class RegisterBedrijfRequestDto : RegisterRequestDto {
    public string NaamBedrijf { get; set; }
    public string Postcode { get; set; }
    public string Plaats { get; set; }
    public string Nummer { get; set; }
    public string WebsiteUrl { get; set; }
    public string Omschrijving { get; set; }
  }
}
