namespace Api.Models.DTO.Auth.request {

  public class RegisterBedrijfRequestDto : RegisterRequestDto {

    public string Bedrijfsnaam { get; set; }
    public string Postcode { get; set; }
    public string Plaats { get; set; }
    public string straat { get; set; }
    public string Nummer { get; set; }
    public string WebsiteUrl { get; set; }
    public string Omschrijving { get; set; }

  }

}
