using Api.Models.Domain.Research;

namespace Api.Models.DTO.Onderzoek;
public class UpdateVraagRequestDto {


  public string? Type { get; set; }
  public string? Onderwerp { get; set; }
  public ICollection<Antwoord>? Antwoorden { get; } = new List<Antwoord>();

}
