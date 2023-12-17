using Api.Models.Domain.Research;
using Api.Models.DTO.Onderzoek;
using Api.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("[controller]")]
[ApiController]
public class OnderzoekController : ControllerBase {

  private IOnderzoekRepository _onderzoekRepository;
  private IMapper _mapper;

  public OnderzoekController(IMapper _mapper,IOnderzoekRepository _onderzoekRepository) {

    this._mapper = _mapper;
    this._onderzoekRepository = _onderzoekRepository;

  }
  [HttpGet]
  [Route("getAll")]
  public async Task<ActionResult<IEnumerable<OnderzoekDto>>> GetAll(string status) {
    var onderzoeken = await _onderzoekRepository.GetAllAsync(status);
    var onderzoekDtos = _mapper.Map<IEnumerable<OnderzoekDto>>(onderzoeken);
    return Ok(onderzoekDtos);

  }
  
  
  [HttpGet]
  [Route("{id}")] 
  public async Task<ActionResult<OnderzoekDto>> GetById(Guid id)
  {
    var onderzoek = await _onderzoekRepository.GetByIdAsync(id);
    if (onderzoek == null)
    {
      return NotFound();
    }
    var onderzoekDto = _mapper.Map<OnderzoekDto>(onderzoek);
    return Ok(onderzoekDto);
  }

  [HttpPost]
  [Route("/create")]
  public async Task<ActionResult<OnderzoekDto>> Create([FromBody] AddOnderzoekRequestDto addDto) 
  {
    var onderzoek = _mapper.Map<Onderzoek>(addDto);
    onderzoek = await _onderzoekRepository.CreateAsync(onderzoek);
    var onderzoekDto = _mapper.Map<OnderzoekDto>(onderzoek);
    return CreatedAtAction(nameof(GetById), new { id = onderzoekDto.Id }, onderzoekDto);
  }
  
 
  [HttpPut]
  [Route("update/{id}")]
  public async Task<IActionResult> Update(Guid id, [FromBody] UpdateOnderzoekRequestDto updateDto)
  {
    if (id != updateDto.Id)
    {
      return BadRequest();
    }

    var onderzoek = _mapper.Map<Onderzoek>(updateDto);
    var bijgewerktOnderzoek = await _onderzoekRepository.UpdateAsync(id, onderzoek);

    if (bijgewerktOnderzoek == null)
    {
      return NotFound();
    }

    return NoContent();
  }


  [HttpDelete]
  [Route("delete/{id}")]
  public async Task<IActionResult> Delete(Guid id)
  {
    var success = await _onderzoekRepository.DeleteAsync(id);
    if (!success)
    {
      return NotFound();
    }
    return NoContent();
  }





}