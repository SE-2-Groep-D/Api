namespace Api.Models.DTO.Onderzoek.response;
public class MinimalQuestionListDto {
  public Guid Id { get; set; }
  public string Title { get; set; }
  public string Description { get; set; }
  public Guid OnderzoekId { get; set; }
}
