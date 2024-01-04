namespace Api.Models.DTO.Onderzoek.results; 
public class ResultResponseDto {

  public List<ResponseVragenlijstDto> Vragenlijsten { get; set; }
  public List<ResponseTrackingDto> TrackingOnderzoeken { get; set; }

}
