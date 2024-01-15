using Api.Models.Domain.Research.Questionlist;
using Api.Models.DTO.Onderzoek.request;

namespace Api.Repositories.VragenlijstRepository.Answer; 

public interface IPossibleAnswerRepository {

  public Task ManageAnswers(Question question, List<UpdatePossibleAnswerDto>? questionDtos);
  public Task AddAnswers(Guid id, List<UpdatePossibleAnswerDto> questionDtos);
  public Task DeleteAnswers(List<Models.Domain.Research.Questionlist.Answer> questions);
  public Task UpdateAnswers(List<UpdatePossibleAnswerDto> questionDtos);

}
