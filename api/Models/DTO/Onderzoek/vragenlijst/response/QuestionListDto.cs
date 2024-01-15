namespace Api.Models.DTO.Onderzoek.response;
public class QuestionListDto {

  public Guid Id { get; set; }
  public string Title { get; set; }
  public string Description { get; set; }
  public Guid OnderzoekId { get; set; }
  public List<QuestionDto> Questions { get; set; }

}
