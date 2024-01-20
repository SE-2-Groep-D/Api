using System.ComponentModel.DataAnnotations;

namespace API.Models.DTO.Gebruiker.request.classes;
public class VoogdDto {

  public string? Name { get; set; }
  [EmailAddress]
  public string? Email { get; set; }
  [Phone]
  public string? Phonenumber { get; set; }

}
