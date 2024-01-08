using System.Text.Json;
using System.Text.Json.Serialization;
using Api.Data;
using Api.Models.Domain.Research;
using Api.Models.DTO.Onderzoek;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories.VragenlijstRepository;
public class SQLVragenlijstRepository : IVragenlijstRepository {

  private AccessibilityDbContext _context;

  private readonly IMapper _mapper;

  public SQLVragenlijstRepository(AccessibilityDbContext context, IMapper mapper) {
    _context = context;
    _mapper = mapper;
  }

  public async Task<List<Questionlist>> GetAllAsync(Guid OnderzoekId) {
    return await _context.Questionlist.Where(v => v.OnderzoekId == OnderzoekId).ToListAsync();
  }

  public async Task<Questionlist?> GetByIdAsync(Guid id) {
    var vragenlijst = _context.Questionlist
      .Include(v => v.Questions)
      //  .ThenInclude(v => v.Antwoorden)
      .SingleOrDefault(v => v.Id == id);

    if (vragenlijst == null) {
      return null;
    }

    var vragenlijstDTO = _mapper.Map<Questionlist>(vragenlijst);
    await AddResearchInfo(vragenlijst, vragenlijstDTO);
    return vragenlijstDTO;
  }

  public async Task AddResearchInfo(Questionlist trackingOnderzoek, Questionlist dto) {
    var onderzoek = await _context.Onderzoeken.FindAsync(trackingOnderzoek.OnderzoekId);
    dto.Participants = (onderzoek == null) ? 0 : onderzoek.AantalParticipanten;
    dto.TotalQuestions = (int)trackingOnderzoek.Questions.Count();
    //   dto.TotalAwnsers = trackingOnderzoek.Vragen.Sum(vraag => vraag.Antwoorden.Count());

    // dto.TimePerPage = (int)trackingOnderzoek.TrackingResultaten
    //   .Select(resultaten => resultaten.TimeInSeconds)
    //   .DefaultIfEmpty(0)
    //   .Average() / 60;
  }

  public async Task<Questionlist> CreateAsync(Questionlist vragenlijst) {
    await _context.AddAsync(vragenlijst);
    await _context.SaveChangesAsync();
    return vragenlijst;
  }

  public async Task<Questionlist?> UpdateAsync(Guid id, Questionlist updatedQuestionlist) {
    var bestaandVragenlijst = await GetByIdAsync(id);
    _context.Entry(bestaandVragenlijst).CurrentValues.SetValues(updatedQuestionlist);
    await _context.SaveChangesAsync();
    return bestaandVragenlijst;
  }

  public async Task<bool> DeleteAsync(Guid id) {
    var vragenlijst = await _context.Questionlist
      .Include(q => q.Questions)
      .ThenInclude(question => question.PossibleAnswers)
      .FirstOrDefaultAsync(q => q.Id == id);

    if (vragenlijst == null) {
      return false;
    }

    // Verwijder alle antwoorden die specifiek bij de vragen van deze vragenlijst horen
    foreach (var question in vragenlijst.Questions) {
      if (question.PossibleAnswers != null) {
        _context.Answer.RemoveRange(question.PossibleAnswers);
      }
    }

    // Verwijder de vragen die specifiek bij deze vragenlijst horen
    _context.Question.RemoveRange(vragenlijst.Questions);

    // Verwijder de vragenlijst zelf
    _context.Questionlist.Remove(vragenlijst);

    await _context.SaveChangesAsync();
    return true;
  }


 /* public async Task<SubmitedRequestDto> CreateSubmittedAnswerAsync(SubmitedRequestDto submittedRequestDto) {
    var questionList = await _context.Questionlists
      .Include(ql => ql.Questions)
      .ThenInclude(q => q.GivenAnswers)
      .FirstOrDefaultAsync(ql => ql.Id == submittedRequestDto.Id);

    if (questionList == null) {
      throw new Exception("Vragenlijst niet gevonden.");
    }

    foreach (var submittedAnswer in submittedRequestDto.GivenAnswers) {
      var question = questionList.Questions.FirstOrDefault(q => q.Id == submittedAnswer.GivenAnswerQuestionId);
      if (question != null) {
        Answer answer = new Answer {
          GivenAnswerQuestionId = question.Id,
          Answertext = submittedAnswer.Answertext
        };

        switch (question.Type) {
          case QuestionType.open:
            // Verwerk het antwoord als een string
            question.GivenAnswers.Add(answer);
            break;

          case QuestionType.one_choice:
          case QuestionType.multiple_choice:
            // Verwerk het antwoord als een array
            // Hier moet je logica toevoegen om de array te verwerken
            // Bijvoorbeeld, als Answertext een JSON-geformatteerde string is
            var answerArray = JsonConverter.DeserializeObject<List<int>>(submittedAnswer.Answertext);
            foreach (var ans in answerArray) {
              // Voeg elk antwoord toe
              question.GivenAnswers.Add(new Answer {
                GivenAnswerQuestionId = question.Id,
                Answertext = ans.ToString() // Of een andere representatie van het antwoord
              });
            }

            break;
        }
      }
    }

    await _context.SaveChangesAsync();

    return submittedRequestDto;
  }
*/
}