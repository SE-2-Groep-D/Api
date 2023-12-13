namespace API.Models.DTO.Gebruiker; 

public class UpdateGebruikerRequestDto {

    public string? Email { get; set; }
    public string? Voornaam { get; set; }
    public string? Achternaam { get; set; }
    public string[]? Roles { get; set; }


    
}