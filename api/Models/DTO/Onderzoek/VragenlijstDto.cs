using Api.Models.Domain.Research;
using System;
using System.Collections.Generic;

namespace Api.Models.DTO.Onderzoek;
public class VragenlijstDto {
  public Guid Id { get; set; }
  public string Titel { get; set; }
  public string Samenvatting { get; set; }
  public Guid OnderzoekId { get; set; }
  public ICollection<Vraag> Vragen { get; } = new List<Vraag>();
  public int? Participants { get; set; }
  public int? TotalQuestions { get; set; }
  public int? TotalAwnsers { get; set; }
}
