using Api.Models.Domain.Research;
using System;
using System.Collections.Generic;

namespace Api.Models.DTO.Onderzoek;
public class VragenlijstDto {
  public Guid Id { get; set; }
  public string Titel { get; set; }
  public string Samenvatting { get; set; }
  public Guid OnderzoekId { get; set; }
  public IEnumerable<VraagDTO> Vragen { get; set; }
  public int? Participants { get; set; }
  public int? TotalQuestions { get; set; }
  public int? TotalAwnsers { get; set; }
}

public class VraagDTO {
  public Guid Id { get; set; }
  public string Type { get; set; }
  public string Onderwerp { get; set; }
  public IEnumerable<AntwoordDTO> Antwoorden { get; set; }
  
}

public class AntwoordDTO {
  public Guid Id { get; set; }
  public string Tekst { get; set; }
}
