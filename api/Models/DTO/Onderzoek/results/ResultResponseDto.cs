using Api.Models.DTO.Onderzoek.response;

namespace Api.Models.DTO.Onderzoek.results;
public class ResultResponseDto {

  public List<ResponseQuestionListDto> QuestionList { get; set; }
  public List<ResponseTrackingDto> TrackingResearches { get; set; }

}
