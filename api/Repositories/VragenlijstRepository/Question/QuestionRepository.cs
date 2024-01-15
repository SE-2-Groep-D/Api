using Api.Data;
using Api.Models.Domain.Research.Questionlist;
using Api.Models.DTO.Onderzoek.request;
using Api.Repositories.VragenlijstRepository.Answer;
using AutoMapper;

namespace Api.Repositories.VragenlijstRepository; 
public class SQLQuestionRepository : IQuestionRepository {

  private readonly IPossibleAnswerRepository _possibleAnswerRepository;
  private AccessibilityDbContext _context;
  private readonly IMapper _mapper;
  
  public SQLQuestionRepository(AccessibilityDbContext context, IMapper mapper, IPossibleAnswerRepository possibleAnswerRepository) {
    _possibleAnswerRepository = possibleAnswerRepository;
    _context = context;
    _mapper = mapper;
  }

  public async Task ManageQuestions(QuestionList questionList, List<UpdateQuestionDto>? questionDtos) {
    if (questionDtos == null) return;
    var questionsToDelete = questionList.Questions.Where(q => questionDtos.SingleOrDefault(dto => dto.Id.Equals(q.Id)) == null).ToList();
    var questionsToUpdate = questionDtos.Where(q => questionList.Questions.SingleOrDefault(dto => dto.Id.Equals(q.Id)) != null).ToList();
    var questionsToAdd = questionDtos.Where(dto => dto.Id == null).ToList();
    
    await DeleteQuestions(questionsToDelete);
    await UpdateQuestions(questionsToUpdate);
    await AddQuestions(questionList.Id, questionsToAdd);
  }

  public async Task UpdateQuestions(List<UpdateQuestionDto> questionDtos) {
    foreach (var updateQuestionDto in questionDtos) {
      var question = _context.Question.SingleOrDefault(q => q.Id.Equals(updateQuestionDto.Id));
      if (updateQuestionDto.Id == null || question == null) continue;
      await _possibleAnswerRepository.ManageAnswers(question, updateQuestionDto.PossibleAnswers);
      _mapper.Map(updateQuestionDto, question);
    }

    await _context.SaveChangesAsync();
  }

  public async Task DeleteQuestions(List<Question> questions) {
    await _possibleAnswerRepository.DeleteAnswers(questions.SelectMany(q => q.PossibleAnswers).ToList());
    _context.Question.RemoveRange(questions);
    await _context.SaveChangesAsync();
  }

  public async Task AddQuestions(Guid id, List<UpdateQuestionDto> questionDtos) {
    foreach (var updateQuestionDto in questionDtos) {
      if (updateQuestionDto.Id != null) continue;
      
      var mappedQuestion = _mapper.Map<Question>(updateQuestionDto);
      mappedQuestion.QuestionListId = id;
      _context.Question.Add(mappedQuestion);
    }

    await _context.SaveChangesAsync();
  }

}
