using Api.Models.Domain.Research;

namespace Api.Models.DTO.Onderzoek;
public class AddVraagRequestDto {

  public VraagType Type { get; set; }
  public string Onderwerp { get; set; }
  public Guid VragenlijstId { get; set; }
  public ICollection<Antwoord> Antwoorden { get; } = new List<Antwoord>();

}

