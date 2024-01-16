namespace Api.Models.DTO.Onderzoek.response;

public class BigQuestionListDto : QuestionListDto {
  public int Participants { get; set; }
  public int TotalQuestions { get; set; }
  public int TotalAnwsers { get; set; }
}
