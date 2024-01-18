using System.ComponentModel.DataAnnotations;

namespace Api.Models.DTO.Auth.request;
public class RegisterMedewerkerRequestDto : RegisterRequestDto {
  [Required]
  public string Functie { get; set; }

}
