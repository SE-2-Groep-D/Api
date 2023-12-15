namespace Api.Models.DTO.Auth
{
    public class LoginResponseDto
    {
        public Guid UserId { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public string JwtToken { get; set; }

    }
}
