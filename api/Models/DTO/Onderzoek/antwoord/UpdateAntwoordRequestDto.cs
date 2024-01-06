using Api.Models.Domain.Research;

namespace Api.Models.DTO.Onderzoek;
public class UpdateAntwoordRequestDto {

  public string? Tekst { get; set; }
  public bool? IsCorrect { get; set; }
}