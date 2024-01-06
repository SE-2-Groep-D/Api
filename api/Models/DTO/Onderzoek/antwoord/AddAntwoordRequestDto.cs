using Api.Models.Domain.Research;

namespace Api.Models.DTO.Onderzoek;
public class AddAntwoordRequestDto {


  public string Tekst { get; set; }
  public bool IsCorrect { get; set; }


}