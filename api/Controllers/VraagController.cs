using Api.Models.Domain.Research;
using Api.Models.DTO.Onderzoek;
using Api.Repositories.VragenRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
[Route("[controller]")]
[ApiController]
public class VraagController : ControllerBase {

  private readonly IMapper _mapper;

  private readonly IVraagRepository _vraagRepository;


  public VraagController(IMapper _mapper, IVraagRepository _vragenlijstRepository) {
    this._mapper = _mapper;
    _vraagRepository = _vragenlijstRepository;
  }

  [HttpGet]
  [Route("list")]
  public async Task<ActionResult> GetAllVragen(Guid VragenLijstId) {
    var vragenlijst = await _vraagRepository.GetAllAsync(VragenLijstId);
    var vragenlijstDtos = _mapper.Map<IEnumerable<VraagDTO>>(vragenlijst);
    return Ok(vragenlijstDtos);
  }

  [HttpGet]
  [Route("{id}")]
  public async Task<ActionResult> GetById(Guid id) {
    var vraag = await _vraagRepository.GetByIdAsync(id);
    if (vraag == null) {
      return NotFound("vraag is niet gevonden met de " + id);
    }

    var vraagDto = _mapper.Map<VraagDTO>(vraag);
    return Ok(vraagDto);
  }

  [HttpPost]
  [Route("create")]
  public async Task<ActionResult<VraagDTO>> Create([FromBody] AddVraagRequestDto addDto) {
    if (!Enum.IsDefined(typeof(VraagType), addDto.Type)) {
      return BadRequest("Invalid VraagType");
    }

    var vraag = _mapper.Map<Vraag>(addDto);
    var nieuwVraag = await _vraagRepository.CreateAsync(vraag);
    var nieuwVraagDto = _mapper.Map<VraagDTO>(nieuwVraag);
    return CreatedAtAction(nameof(GetById), new { id = nieuwVraagDto.Id }, nieuwVraagDto);
  }

  [HttpPut]
  [Route("update/{id}")]
  public async Task<IActionResult> Update(Guid id, [FromBody] UpdateVraagRequestDto request) {
    try {
      var bestaandVraag = await _vraagRepository.GetByIdAsync(id);

      if (bestaandVraag == null) {
        return NotFound($"Vraag met ID {id} is niet gevonden.");
      }

      _mapper.Map(request, bestaandVraag);

      var isUpdated = await _vraagRepository.UpdateAsync(id, bestaandVraag);

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
