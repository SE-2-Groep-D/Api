namespace Api.Models.DTO.Gebruiker.response;
public class UpdateGebruikerResponse {

  public UpdateGebruikerResponse(bool succeeded, string? message) {
    Succeeded = succeeded;
    Message = message;
  }

  public string? Message { get; set; }
  public bool Succeeded { get; set; }
}
