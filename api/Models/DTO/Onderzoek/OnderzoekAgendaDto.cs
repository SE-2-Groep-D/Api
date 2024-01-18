using Api.CustomActionFilters.CustomAttributes;

namespace Api.Models.DTO.Onderzoek;
public class OnderzoekAgendaDto {

  public Guid Id { get; set; }

  public string Company { get; set; }
  public string Title { get; set; }
  public int Participants { get; set; }
  public DateTime Date { get; set; }
  [AllowedValues("open", "active", "ended")]
  public string? Status { get; set; }

}
