using Api.Models.Domain.Research;
using Api.Models.DTO.Onderzoek;
using Api.Repositories.VragenlijstRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("[controller]")]
[ApiController]
public class VragenlijstController : ControllerBase  {

  private IVragenlijstRepository _vragenlijstRepository;
  private IMapper _mapper;
  
  
  public VragenlijstController(IMapper _mapper,IVragenlijstRepository _vragenlijstRepository) {

    this._mapper = _mapper;
    this._vragenlijstRepository = _vragenlijstRepository;

  }
  
  [HttpGet]
  [Route("list")]
  public async Task<ActionResult> GetAll(Guid OnderzoekId) {
    var vragenlijsten = await _vragenlijstRepository.GetAllAsync(OnderzoekId);
    var vragenlijstenDtos = _mapper.Map<IEnumerable<VragenlijstDto>>(vragenlijsten);
    return Ok(vragenlijstenDtos);

  }
  
  [HttpGet]
  [Route("{id}")] 
  public async Task<ActionResult> GetById(Guid id)
  {
    var vragenlijsten = await _vragenlijstRepository.GetByIdAsync(id);
    if (vragenlijsten == null)
    {
      return NotFound();
    }
    var vragenlijstenDto = _mapper.Map<VragenlijstDto>(vragenlijsten);
    return Ok(vragenlijstenDto);
  }
  
  [HttpPost]
  [Route("create")]
  public async Task<ActionResult<VragenlijstDto>> Create([FromBody] AddVragenlijstRequestDto addDto) 
  {
    var vragenlijsten = _mapper.Map<Vragenlijst>(addDto);
    vragenlijsten = await _vragenlijstRepository.CreateAsync(vragenlijsten);
    var vragenlijstenDto = _mapper.Map<VragenlijstDto>(vragenlijsten);
    return CreatedAtAction(nameof(GetById), new { id = vragenlijstenDto.Id }, vragenlijstenDto);
  }
  
  [HttpPut]
  [Route("update/{id}")]
  public async Task<IActionResult> Update(Guid id, [FromBody] UpdateVragenlijstRequestDto request)
  {
    try
    {
      Vragenlijst? bestaandVragenlijst = await _vragenlijstRepository.GetByIdAsync(id);

      if (bestaandVragenlijst == null)
      {
        return NotFound($"Vragenlijst met ID {id} is niet gevonden.");
      }

      _mapper.Map(request, bestaandVragenlijst);

      Vragenlijst? isUpdated = await _vragenlijstRepository.UpdateAsync(id,bestaandVragenlijst);

      if (isUpdated==null)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Er is een fout opgetreden bij het bijwerken van het onderzoek.");
      }

      return Ok(isUpdated);
    }
    catch (Exception ex)
    {
      var errorMsg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;

      return StatusCode(StatusCodes.Status500InternalServerError, $"Interne serverfout: {errorMsg}");
    }
  }

  [HttpDelete]
  [Route("delete/{id}")]
  public async Task<IActionResult> Delete(Guid id)
  {
    var success = await _vragenlijstRepository.DeleteAsync(id);
    if (!success)
    {
      return NotFound();
    }
     return Ok("Vragenlijst is succsesvol updated");
  }

  
  

}