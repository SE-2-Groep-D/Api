namespace Api.Models.DTO.Auth.response {
  public class RegisterResponseDto {

    public RegisterResponseDto(bool succeeded, string? message) {
      Succeeded = succeeded;
      Message = message;
    }

    public string? Message { get; set; }
    public bool Succeeded { get; set; }
    public Guid? Id { get; set; }

  }
}

