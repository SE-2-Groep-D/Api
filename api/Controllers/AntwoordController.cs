using Api.Models.Domain.Research;
using Api.Models.DTO.Onderzoek;
using Api.Repositories.AntwoordRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
[Route("[controller]")]
[ApiController]
public class AntwoordController : ControllerBase{
  
  private IAntwoordRepository _antwoordRepository;
  private IMapper _mapper;


  public AntwoordController(IMapper _mapper,IAntwoordRepository _antwoordRepository) {
    
    this._mapper = _mapper;
    this._antwoordRepository = _antwoordRepository;
  }
  
  
  [HttpGet]
  [Route("list")]
  public async Task<ActionResult> GetAll(Guid id) {
    var antworden = await _antwoordRepository.GetAllAsync(id);
    var antwordenDtos = _mapper.Map<IEnumerable<AntwoordDTO>>(antworden);
    return Ok(antwordenDtos);
  }
  
  
  [HttpGet]
  [Route("{id}")]
  public async Task<ActionResult> GetById(Guid id) {
    var antwoord = await _antwoordRepository.GetByIdAsync(id);
    if (antwoord == null) {
      return NotFound();
    }

    var antwoordDto = _mapper.Map<AntwoordDTO>(antwoord);
    return Ok(antwoordDto);
  }
  
  [HttpPost]
  [Route("create")]
  public async Task<ActionResult<AntwoordDTO>> Create([FromBody] AddAntwoordRequestDto addDto) {
    var antwoord = _mapper.Map<Antwoord>(addDto);
    var nieuwAntwoord = await _antwoordRepository.CreateAsync(antwoord);
    var nieuwAntwoordDto = _mapper.Map<AntwoordDTO>(nieuwAntwoord);
    return CreatedAtAction(nameof(GetById), new { id = nieuwAntwoordDto.Id }, nieuwAntwoordDto);
  }
  
  [HttpPut]
  [Route("update/{id}")]
  public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAntwoordRequestDto request) {
    try {
      Antwoord? bestaandAntwoord = await _antwoordRepository.GetByIdAsync(id);

      if (bestaandAntwoord == null) {
        return NotFound($"Vraag met ID {id} is niet gevonden.");
      }

      _mapper.Map(request, bestaandAntwoord);

      Antwoord? isUpdated = await _antwoordRepository.UpdateAsync(id, bestaandAntwoord);

      if (isUpdated == null) {
        return StatusCode(StatusCodes.Status500InternalServerError,
          "Er is een fout opgetreden bij het bijwerken van het antwoord.");
      }

      return Ok(isUpdated);
    } catch (Exception ex) {
      var errorMsg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;

      return StatusCode(StatusCodes.Status500InternalServerError, $"Interne serverfout: {errorMsg}");
    }
  }
  
  [HttpDelete]
  [Route("delete/{id}")]
  public async Task<IActionResult> Delete(Guid id) {
    var success = await _antwoordRepository.DeleteAsync(id);
    if (!success) {
      return NotFound();
    }

    return Ok("Antwoord is succsesvol verwijderd");
  }
  
  



}