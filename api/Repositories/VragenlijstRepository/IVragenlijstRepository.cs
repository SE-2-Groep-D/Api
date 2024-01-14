using Api.Models.Domain.Research.Questionlist;
using Api.Models.DTO.Onderzoek.request;

namespace Api.Repositories.VragenlijstRepository;
public interface IVragenlijstRepository {


  Task<List<QuestionList>> GetAllAsync(Guid onderzoekId);
  Task<QuestionList?> GetByIdAsync(Guid id);

  Task<QuestionList?> CreateAsync(CreateQuestionListDto questionListDto);


 Task<QuestionList?> UpdateAsync(Guid id, UpdateQuestionListDto updateDto);

  Task<bool> DeleteAsync(Guid id);

}
