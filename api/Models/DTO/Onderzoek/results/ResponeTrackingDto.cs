using Api.Models.Domain.Research.Tracking;
using Api.Models.DTO.Onderzoek.tracking;

namespace Api.Models.DTO.Onderzoek.results; 
public class ResponseTrackingDto {

  public Guid Id { get; set; }
  public int Participants { get; set; }
  public int ScrollPercentage { get; set; }
  public int TimePerPage { get; set; }
  
  public string Domain { get; set; }
  public Guid OnderzoekId { get; set; }

  public ICollection<TrackingResultaten> TrackingResultaten { get; set; } = new List<TrackingResultaten>();
  public ICollection<OtherResult> OtherResults { get; set; } = new List<OtherResult>();
}
