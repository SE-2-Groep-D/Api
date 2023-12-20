using Api.Models.Domain.User;

namespace Api.Models.DTO.Bericht {
  public class StuurBerichtRequestDto {

    public string Tekst { get; set; }
    public Guid VerzenderId { get; set; }
    public Guid OntvangerId { get; set; }

  }
}
