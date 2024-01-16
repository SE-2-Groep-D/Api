namespace Api.Models.DTO.Onderzoek.response; 
public class ResponseAnswerDto {

  public Guid Id { get; set; }
  public string Value { get; set; }
  public Guid QuestionId { get; set; }
}