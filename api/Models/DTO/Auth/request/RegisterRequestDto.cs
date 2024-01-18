using System.ComponentModel.DataAnnotations;

namespace Api.Models.DTO.Auth.request; 
public class RegisterRequestDto {
    [Required]
    public string Voornaam { get; set; }
    public string Achternaam { get; set; }
    public bool? GoogleAccount { get; set; } = false;
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [DataType(DataType.Password)]
    public string? Password { get; set; }
    public string[]? Roles { get; set; }

}
