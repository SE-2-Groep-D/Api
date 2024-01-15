using Api.Models.Domain.Bericht;

namespace Api.Models.DTO.Berichten {
  public class ChatResponseDto {
    public Guid OtherUserId { get; set; }
    public Bericht LastMessage { get; set; }
    public int TotalMessages { get; set; }

  }
}
