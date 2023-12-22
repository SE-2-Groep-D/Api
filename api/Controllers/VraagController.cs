using Api.Models.Domain.Research;
using Api.Models.DTO.Onderzoek;
using Api.Repositories.VragenRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
[Route("[controller]")]
[ApiController]
public class VraagController : ControllerBase {

  private IVraagRepository _vraagRepository;
  private IMapper _mapper;


  public VraagController(IMapper _mapper, IVraagRepository _vragenlijstRepository) {
    this._mapper = _mapper;
    this._vraagRepository = _vragenlijstRepository;
  }

  [HttpGet]
  [Route("list")]
  public async Task<ActionResult> GetAllVragen(Guid VragenLijstId) {
    var vragenlijst = await _vraagRepository.GetAllAsync(VragenLijstId);
    var vragenlijstDtos = _mapper.Map<IEnumerable<VraagDto>>(vragenlijst);
    return Ok(vragenlijstDtos);
  }

  [HttpGet]
  [Route("{id}")]
  public async Task<ActionResult> GetById(Guid id) {
    var vraag = await _vraagRepository.GetByIdAsync(id);
    if (vraag == null) {
      return NotFound("vraag is niet gevonden met de " + id);
    }

    var vraagDto = _mapper.Map<VraagDto>(vraag);
    return Ok(vraagDto);
  }

  [HttpPost]
  [Route("create")]
  public async Task<ActionResult<VraagDto>> Create([FromBody] AddVraagRequestDto addDto) {
    var vraag = _mapper.Map<Vraag>(addDto);
    var nieuwVraag = await _vraagRepository.CreateAsync(vraag);
    var nieuwVraagDto = _mapper.Map<VraagDto>(nieuwVraag);
    return CreatedAtAction(nameof(GetById), new { id = nieuwVraagDto.Id }, nieuwVraagDto);
  }

  [HttpPut]
  [Route("update/{id}")]
  public async Task<IActionResult> Update(Guid id, [FromBody] UpdateVraagRequestDto request) {
    try {
      Vraag? bestaandVraag = await _vraagRepository.GetByIdAsync(id);

      if (bestaandVraag == null) {
        return NotFound($"Vraag met ID {id} is niet gevonden.");
      }

      _mapper.Map(request, bestaandVraag);

      Vraag? isUpdated = await _vraagRepository.UpdateAsync(id, bestaandVraag);

      if (isUpdated == null) {
        return StatusCode(StatusCodes.Status500InternalServerError,
          "Er is een fout opgetreden bij het bijwerken van het vraag.");
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
    var success = await _vraagRepository.DeleteAsync(id);
    if (!success) {
      return NotFound();
    }

    return Ok("Vraag is succsesvol verwijderd");
  }

}