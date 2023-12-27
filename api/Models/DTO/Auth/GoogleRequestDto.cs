using System.ComponentModel.DataAnnotations;

namespace Api.Models.DTO.Auth {
  public class GoogleRequestDto {
    [Required]
    public string IdToken { get; set; }
  }
}
