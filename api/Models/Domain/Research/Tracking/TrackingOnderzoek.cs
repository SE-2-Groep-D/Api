using Api.Models.DTO.Onderzoek.tracking;

namespace Api.Models.Domain.Research.Tracking;
public class TrackingOnderzoek {

  public Guid Id { get; set; }
  public string Domain { get; set; }
  public Guid OnderzoekId { get; set; }
  public ICollection<TrackingResultaten> TrackingResultaten { get; set; } = new List<TrackingResultaten>();

}
