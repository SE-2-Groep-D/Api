using Api.Models.Domain.Research.Questionlist;
using Api.Models.DTO.Onderzoek.request;

namespace Api.Repositories.VragenlijstRepository; 
public interface IQuestionRepository {

  public Task ManageQuestions(QuestionList questionList, List<UpdateQuestionDto>? questionDtos);
  public Task AddQuestions(Guid id, List<UpdateQuestionDto> questionDtos);
  public Task DeleteQuestions(List<Question> questions);
  public Task UpdateQuestions(List<UpdateQuestionDto> questionDtos);

}
