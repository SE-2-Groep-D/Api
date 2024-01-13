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

       var questionlist = _mapper.Map<Questionlist>(requestDto);
       questionlist = await _vragenlijstRepository.UpdateAsync(id,questionlist);

       if (questionlist == null) {
         return StatusCode(StatusCodes.Status500InternalServerError, "Er is een fout opgetreden bij het bijwerken van het onderzoek.");
       }


       return Ok(questionlist);
     }  catch (Exception ex) {
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

