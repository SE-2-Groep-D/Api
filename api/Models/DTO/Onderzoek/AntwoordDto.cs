using Api.Models.Domain.Research;

namespace Api.Models.DTO.Onderzoek;
public class AntwoordDto {

  public Guid Id { get; set; }
  public string Tekst { get; set; }
  public Guid VraagId { get; set; } 
  public Vraag Vraag { get; set; } = null!;

}