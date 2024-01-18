using Api.Models.Domain.Research.Questionlist;
using Api.Models.DTO.Onderzoek.request;
using Api.Models.DTO.Onderzoek.response;

namespace Api.Repositories.VragenlijstRepository;
public interface IVragenlijstRepository {


  Task<List<QuestionList>> GetAllAsync(Guid onderzoekId);
  Task<QuestionList?> GetByIdAsync(Guid id);

  Task<QuestionList?> CreateAsync(CreateQuestionListDto questionListDto);


 Task<QuestionList?> UpdateAsync(Guid id, UpdateQuestionListDto updateDto);

  Task<bool> DeleteAsync(Guid id);
  Task<BigQuestionListDto?> GetInfo(Guid id);

  Task<QuestionList?> SubmitAnswers(Guid id, SubmitAnswersDto dto);
  Task<List<Models.Domain.Research.Questionlist.Answer>> GetAnswers(Guid questionListId);

}
