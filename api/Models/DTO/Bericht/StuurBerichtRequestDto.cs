using Api.Models.Domain.User;
using System.ComponentModel.DataAnnotations;

namespace Api.Models.DTO.Bericht {
  public class StuurBerichtRequestDto {

    [MaxLength(2000, ErrorMessage = "Bericht mag niet langer zijn dan {1} karakters")]
    public string Tekst { get; set; }
    public Guid VerzenderId { get; set; }
    public Guid OntvangerId { get; set; }

  }
}
