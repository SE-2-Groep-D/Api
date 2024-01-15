using Api.Data;
using Api.Models.Domain.Research.Questionlist;
using Api.Models.DTO.Onderzoek.request;
using AutoMapper;

namespace Api.Repositories.VragenlijstRepository.Answer; 
public class SQLPossibleAnswerRepository : IPossibleAnswerRepository {
  
  private AccessibilityDbContext _context;
  private readonly IMapper _mapper;
  
  public SQLPossibleAnswerRepository(AccessibilityDbContext context, IMapper mapper) {
    _context = context;
    _mapper = mapper;
  }

  public async Task ManageAnswers(Question question, List<UpdatePossibleAnswerDto>? questionDtos) {
    if (questionDtos == null) return;
    var delete = question.PossibleAnswers.Where(q => questionDtos.SingleOrDefault(dto => dto.Id.Equals(q.Id)) == null).ToList();
    var update = questionDtos.Where(q => question.PossibleAnswers.SingleOrDefault(dto => dto.Id.Equals(q.Id)) != null).ToList();
    var add = questionDtos.Where(dto => dto.Id == null).ToList();
    
    await DeleteAnswers(delete);
    await UpdateAnswers(update);
    await AddAnswers(question.Id, add);
  }

  public async Task UpdateAnswers(List<UpdatePossibleAnswerDto> questionDtos) {
    foreach (var updateQuestionDto in questionDtos) {
      var question = _context.Question.SingleOrDefault(q => q.Id.Equals(updateQuestionDto.Id));
      if (updateQuestionDto.Id == null || question == null) continue;
      _mapper.Map(updateQuestionDto, question);
    }

    await _context.SaveChangesAsync();
  }

  public async Task DeleteAnswers(List<Models.Domain.Research.Questionlist.Answer> answers) {
    _context.Answers.RemoveRange(answers);
    await _context.SaveChangesAsync();
  }

  public async Task AddAnswers(Guid id, List<UpdatePossibleAnswerDto> dtos) {
    foreach (var updateDto in dtos) {
      if (updateDto.Id != null) continue;
      
      var mapped = _mapper.Map<Models.Domain.Research.Questionlist.Answer>(updateDto);
      mapped.QuestionId = id;
      _context.Answers.Add(mapped);
    }

    await _context.SaveChangesAsync();
  }

  

}
