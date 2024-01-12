namespace Api.Models.DTO.Onderzoek;
public class AddRegistrationDto {

  public Guid OnderzoekId { get; set; }
  public Guid ErvaringsdeskundigeId { get; set; }
  public DateTime datum { get; set; }

}