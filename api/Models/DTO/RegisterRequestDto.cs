namespace Api.Models.DTO
{
    public class RegisterRequestDto
    {
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public bool GoogleAccount { get; set; } = false;
        public string Email { get; set; }
    }
}
