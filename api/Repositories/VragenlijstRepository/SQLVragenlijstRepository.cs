using Api.Data;
using Api.Models.Domain.Research.Questionlist;
using Api.Models.DTO.Onderzoek.request;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Api.Repositories.VragenlijstRepository;
public class SQLVragenlijstRepository : IVragenlijstRepository {

  private AccessibilityDbContext _context;
  private readonly IMapper _mapper;
  private readonly IQuestionRepository _questionRepository;


  public SQLVragenlijstRepository(AccessibilityDbContext context, IMapper mapper, IQuestionRepository questionRepository) {
    _questionRepository = questionRepository;
    _context = context;
    _mapper = mapper;
  }

  public async Task<List<QuestionList>> GetAllAsync(Guid onderzoekId) {
    return await _context.Questionlist.Where(l => l.OnderzoekId == onderzoekId).Include(q => q.Questions).ToListAsync();
  }

  public async Task<QuestionList?> GetByIdAsync(Guid questionListId) {
    return await _context.Questionlist.Include(ql => ql.Questions).ThenInclude(q => q.PossibleAnswers).SingleOrDefaultAsync(); 
  }



  public async Task<QuestionList?> CreateAsync(CreateQuestionListDto dto) {
    try {
      Console.WriteLine('1');
      var questionList = _mapper.Map<QuestionList>(dto);
      
      Console.WriteLine('2');
      
      await _context.AddAsync(questionList);
      await _context.SaveChangesAsync();
      
      Console.WriteLine('3');

      return questionList;
    } catch (Exception ex) {
      Console.WriteLine(ex.Message);
      return null;
    }
  }

  public async Task<QuestionList?> UpdateAsync(Guid id, UpdateQuestionListDto dto) {
    var questionList = await GetByIdAsync(id);
    if (questionList == null) return null;
    var itemsToUpdate = dto.GetType().GetProperties().Count(p => p.Name != "Questions" && p.GetValue(dto) != null);

    await _questionRepository.ManageQuestions(questionList, dto.Questions);
    if (itemsToUpdate == 0) {
      return questionList;
    }

    _mapper.Map(dto, questionList);
    await _context.SaveChangesAsync();
    return questionList;
  }


  public async Task<bool> DeleteAsync(Guid guid) {
    var list = await GetByIdAsync(guid);
    if (list == null) return true;
    await _questionRepository.DeleteQuestions(list.Questions);
    _context.Questionlist.Remove(list);
    await _context.SaveChangesAsync();
    return true;
  }

}
