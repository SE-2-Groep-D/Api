using System.Runtime.InteropServices.JavaScript;
using Api.Data;
using Api.Models.Domain.Research.Questionlist;
using Api.Models.DTO.Onderzoek.request;
using Api.Models.DTO.Onderzoek.response;
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
    return await _context.Questionlist.Include(ql => ql.Questions).ThenInclude(q => q.PossibleAnswers).FirstOrDefaultAsync(); 
  }



  public async Task<QuestionList?> CreateAsync(CreateQuestionListDto dto) {
    try {
      var questionList = _mapper.Map<QuestionList>(dto);
      
      await _context.AddAsync(questionList);
      await _context.SaveChangesAsync();

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
  
  public async Task<BigQuestionListDto?> GetInfo(Guid id) {
    var questionList = await _context.Questionlist
      .Include(ql => ql.Onderzoek)
      .Include(ql => ql.Questions)
      .ThenInclude(q => q.GivenAnswers)
      .Include(ql => ql.Questions)
      .ThenInclude(q => q.PossibleAnswers)
      .FirstOrDefaultAsync(ql => ql.Id.Equals(id));

    if (questionList == null) {
      return null;
    }
    var dto = _mapper.Map<BigQuestionListDto>(questionList);
    
    foreach (var dtoQuestion in dto.Questions) {
      var matchingQuestion = questionList.Questions.FirstOrDefault(q => q.Id.Equals(dtoQuestion.Id));
      dtoQuestion.TotalAnswers = matchingQuestion?.GivenAnswers.Count ?? 0;
    }
    
    
    dto.Participants = questionList.Onderzoek.Ervaringsdeskundigen?.Count ?? 0;
    dto.TotalQuestions = questionList.Questions?.Count ?? 0;
    dto.TotalAnwsers = questionList?.Questions.Sum(q => q.GivenAnswers.Count) ?? 0;
    
    return dto;
  }

  public async Task<QuestionList?> SubmitAnswers(Guid id, SubmitAnswersDto dto) {
    var list = await _context.Questionlist.Include(ql => ql.Questions).ThenInclude(q => q.GivenAnswers).SingleOrDefaultAsync(q => q.Id.Equals(id));
    if (list == null) {
      return null;
    }
    
    var answers = _mapper.Map<List<Models.Domain.Research.Questionlist.Answer>>(dto.Answers);
    if (answers == null) {
      throw new Exception("Could not map Answer from list.");
    }
    
    var answerGroups = answers.GroupBy(answer => answer.QuestionId);
    foreach (var group in answerGroups) {
      var question = list.Questions.FirstOrDefault(q => q.Id.Equals(group.Key));
      Console.WriteLine(question == null);
      Console.WriteLine(group.Key);
      if(question == null) continue;
      question.GivenAnswers.AddRange(group);
    }
    
    await _context.SaveChangesAsync();
    return list;
  }

  public async Task<List<Models.Domain.Research.Questionlist.Answer>> GetAnswers(Guid questionListId) {
    return await _context.Answers.Where(answer => answer.Question.QuestionListId.Equals(questionListId)).ToListAsync();;
  }

}
