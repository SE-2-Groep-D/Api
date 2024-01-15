using Api.Models.Domain.Research.Questionlist;

namespace Api.Models.DTO.Onderzoek.response;
public class QuestionDto {

  public Guid Id { get; set; }
  public string Type { get; set; }
  public string Description { get; set; }
  public List<AnswerDto> PossibleAnswers { get; set; }
  public List<AnswerDto> GivenAnswers { get; set; }

}
