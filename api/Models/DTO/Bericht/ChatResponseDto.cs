namespace Api.Models.DTO.Bericht;
using Api.Models.Domain.Bericht;
using Api.Models.DTO.Bericht;

public class ChatResponseDto {
  public Guid OtherUserId { get; set; }
  public string? Naam { get; set; }
  public BerichtDto LastMessage { get; set; }
  public int TotalMessages { get; set; }
  public bool? Suceeded { get; set; }
}

