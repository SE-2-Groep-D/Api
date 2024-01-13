using Api.Models.Domain.Research;
using Api.Models.DTO.Onderzoek;
using Api.Repositories.VragenlijstRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Enum = Google.Protobuf.WellKnownTypes.Enum;
using System;

namespace Api.Controllers;
[Route("[controller]")]
[ApiController]
public class VragenlijstController : ControllerBase {

  private readonly IMapper _mapper;

  private readonly IVragenlijstRepository _vragenlijstRepository;


  public VragenlijstController(IMapper mapper, IVragenlijstRepository vragenlijstRepository) {
    _mapper = mapper;
    _vragenlijstRepository = vragenlijstRepository;
  }

  [HttpGet]
  [Route("list")]
  public async Task<ActionResult> GetAll(Guid onderzoekId) {
    var vragenlijsten = await _vragenlijstRepository.GetAllAsync(onderzoekId);
    var vragenlijstenDtos = _mapper.Map<IEnumerable<QuestionlistDto>>(vragenlijsten);
    return Ok(vragenlijstenDtos);
  }

  [HttpGet]
  [Route("{id}")]
  public async Task<ActionResult> GetById(Guid id) {
    var vragenlijsten = await _vragenlijstRepository.GetByIdAsync(id);
    if (vragenlijsten == null) return NotFound();
    var vragenlijstenDto = _mapper.Map<QuestionlistDto>(vragenlijsten);
    return Ok(vragenlijstenDto);
  }

  [HttpPost]
  [Route("create")]
  public async Task<IActionResult> Create([FromBody] AddQuestionlistRequestDto addDto) {
    var questionlist = _mapper.Map<Questionlist>(addDto);
    questionlist = await _vragenlijstRepository.CreateAsync(questionlist);
    var questionlistDto = _mapper.Map<QuestionlistDto>(questionlist);
    return CreatedAtAction(nameof(GetById), new { id = questionlistDto.Id }, questionlistDto);
  }


  [HttpPut]
  [Route("update/{id}")]
  public async Task<IActionResult> Update(Guid id, [FromBody] UpdateQuestionlistRequestDto requestDto) {
    try {
      var questionlist = await _vragenlijstRepository.GetByIdAsync(id); // Aanname dat er een GetByIdAsync methode is
      if (questionlist == null) {
        return NotFound("Questionlist not found");
      }

      // Update velden
      if (requestDto.Title != null) {
        questionlist.Title = requestDto.Title;
      }

      if (requestDto.Description != null) {
        questionlist.Description = requestDto.Description;
      }

      // Update vragen
      if (requestDto.Questions != null && requestDto.Questions.Any()) {
        foreach (var questionDto in requestDto.Questions) {
          var question = questionlist.Questions.FirstOrDefault(q => q.Id == questionDto.Id);
          if (question == null) {
            // Voeg nieuwe vraag toe
            var newQuestion = new Question {
              Id = Guid.NewGuid(),
              Title = questionDto.Title,
              Type = (QuestionType)System.Enum.Parse(typeof(QuestionType), questionDto.Type),
              QuestionlistId = questionlist.Id,
              PossibleAnswers = questionDto.PossibleAnswers.Select(a => new Answer {
                Id = Guid.NewGuid(),
                Answertext = a.Answertext,
                PossibleAnswerQuestionId = question.Id
              }).ToList()
            };
            questionlist.Questions.Add(newQuestion);
          }
          else {
            // Update bestaande vraag
            question.Title = questionDto.Title;
            question.Type = (QuestionType)System.Enum.Parse(typeof(QuestionType), questionDto.Type);


            // Update mogelijke antwoorden
            var updatedAnswerIds = questionDto.PossibleAnswers.Select(a => a.Id).ToList();
            foreach (var existingAnswer in question.PossibleAnswers.ToList()) {
              if (!updatedAnswerIds.Contains(existingAnswer.Id)) {
                question.PossibleAnswers.Remove(existingAnswer);
              }
            }

            foreach (var answerDto in questionDto.PossibleAnswers) {
              var answer = question.PossibleAnswers.FirstOrDefault(a => a.Id == answerDto.Id);
              if (answer == null) {
                // Voeg nieuw antwoord toe
                question.PossibleAnswers.Add(new Answer {
                  Id = Guid.NewGuid(),
                  Answertext = answerDto.Answertext,
                  PossibleAnswerQuestionId = question.Id
                });
              }
              else {
                // Update bestaand antwoord
                answer.Answertext = answerDto.Answertext;
              }
            }
          }
        }
      }

      questionlist = await _vragenlijstRepository.UpdateAsync(id, questionlist);

      if (questionlist == null) {
        return StatusCode(StatusCodes.Status500InternalServerError,
          "Er is een fout opgetreden bij het bijwerken van de vragenlijst.");
      }

      return Ok(questionlist);
    } catch (Exception ex) {
      var errorMsg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
      return StatusCode(StatusCodes.Status500InternalServerError, $"Interne serverfout: {errorMsg}");
    }
  }


  [HttpDelete]
  [Route("delete/{id}")]
  public async Task<IActionResult> Delete(Guid id) {
    var success = await _vragenlijstRepository.DeleteAsync(id);
    if (!success) {
      return NotFound();
    }

    return Ok("Vragenlijst is succsesvol updated");
  }

}