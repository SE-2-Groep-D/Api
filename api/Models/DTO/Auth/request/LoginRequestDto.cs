using System.ComponentModel.DataAnnotations;

namespace Api.Models.DTO.Auth.request; 
public class LoginRequestDto {
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

}
