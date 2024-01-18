using System.ComponentModel.DataAnnotations;
using Api.CustomActionFilters.CustomAttributes;
namespace Api.Models.DTO.Auth.request; 
public class RegisterBedrijfRequestDto : RegisterRequestDto {
    [Required]
    public string Bedrijfsnaam { get; set; }
    [Required]
    [Postcode]
    public string Postcode { get; set; }
    [Required]
    public string Plaats { get; set; }
    [Required]
    public string straat { get; set; }
    [Required]
    public string Nummer { get; set; }
    [Required]
    [Url]
    public string WebsiteUrl { get; set; }
    [Required]
    public string Omschrijving { get; set; }

}
