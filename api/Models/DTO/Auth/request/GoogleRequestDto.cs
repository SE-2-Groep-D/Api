using System.ComponentModel.DataAnnotations;

namespace Api.Models.DTO.Auth.request; 
public class GoogleRequestDto {

  [Required]
  public string IdToken { get; set; }

}
