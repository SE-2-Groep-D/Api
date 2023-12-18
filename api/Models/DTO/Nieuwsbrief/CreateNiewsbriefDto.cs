namespace Api.Models.DTO.Nieuwsbrief; 
public class CreateNiewsbriefDto {

  public DateTime datum { get; set; }
  public string Titel { get; set; }
  public string Inhoud { get; set; }
  public string MedewerkerId { get; set; }

}
