using System.ComponentModel.DataAnnotations;

namespace Api.Models.DTO.Gebruiker;
public class ChangePasswordDto {

  [Required]
  public string password { get; set; }
  [Required]
  public string NewPassword { get; set; }

}
