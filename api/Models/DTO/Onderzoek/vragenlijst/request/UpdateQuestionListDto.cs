namespace Api.Models.DTO.Onderzoek.request; 
public class UpdateQuestionListDto {

  public string? Title { get; set; }
  public string? Description { get; set; }
  public List<UpdateQuestionDto>? Questions { get; set; }
}

public class UpdateQuestionDto {
  
  public Guid? Id { get; set; }
  public string? Type { get; set; }
  public string? Description { get; set; }
  public List<UpdatePossibleAnswerDto>? PossibleAnswers { get; set; }

}

public class UpdatePossibleAnswerDto {

  public Guid? Id { get; set; }
  public string Value { get; set; }

}
