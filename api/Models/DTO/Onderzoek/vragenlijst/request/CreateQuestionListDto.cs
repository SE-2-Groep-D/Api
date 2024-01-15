using Api.Models.Domain.Research.Questionlist;

namespace Api.Models.DTO.Onderzoek.request;
public class CreateQuestionListDto {

  public string Title { get; set; }
  public string Description { get; set; }
  public Guid OnderzoekId { get; set; }
  public List<CreateQuestionDto> Questions { get; set; }
}

public class CreateQuestionDto {
  
  public QuestionType Type { get; set; }
  public string Description { get; set; }
  public List<CreatePossibleAnswerDto> PossibleAnswers { get; set; }

}

public class CreatePossibleAnswerDto {

  public string Value { get; set; }

}
