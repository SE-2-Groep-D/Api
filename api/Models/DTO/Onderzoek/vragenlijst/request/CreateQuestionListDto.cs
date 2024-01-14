namespace Api.Models.DTO.Onderzoek.request;
public class CreateQuestionListDto {

  public string Title { get; set; }
  public string Description { get; set; }
  public Guid OnderzoekId { get; set; }
  public List<CreateQuestionDto> Questions { get; set; }
}

public class CreateQuestionDto {
  
  public string QuestionType { get; set; }
  public string Description { get; set; }
  public List<string> PossibleAnswers { get; set; }

}
