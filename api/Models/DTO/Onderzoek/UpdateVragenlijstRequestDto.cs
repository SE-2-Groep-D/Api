using Api.Models.Domain.Research;

namespace Api.Models.DTO.Onderzoek;
public class UpdateVragenlijstRequestDto {


  public string? Titel { get; set; }
  public string? Samenvatting { get; set; }


  public ICollection<Vraag> Vragen { get; } = new List<Vraag>();

}
