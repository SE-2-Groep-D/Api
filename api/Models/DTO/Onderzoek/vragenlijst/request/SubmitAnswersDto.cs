using Api.Models.Domain.Research.Questionlist;

namespace Api.Models.DTO.Onderzoek.request; 
public class SubmitAnswersDto {

  public List<SubmitAnswerDto> Answers { get; set; }

}

public class SubmitAnswerDto {

  public Guid QuestionId { get; set; }
  public string Value { get; set; }

}