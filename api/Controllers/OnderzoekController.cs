﻿using Api.Models.Domain.Research;
using Api.Models.DTO.Onderzoek;
using Api.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
[Route("[controller]")]
[ApiController]
public class OnderzoekController : ControllerBase {

  private IOnderzoekRepository _onderzoekRepository;
  private IMapper _mapper;


  public OnderzoekController(IMapper mapper, IOnderzoekRepository onderzoekRepository) {
    this._mapper = mapper;
    this._onderzoekRepository = onderzoekRepository;

  }

  [HttpGet]
  [Route("list")]
  public async Task<ActionResult> GetAll(string? status) {
    var onderzoeken = await _onderzoekRepository.GetAllAsync(status);
    var onderzoekDtos = _mapper.Map<IEnumerable<OnderzoekDto>>(onderzoeken);
    return Ok(onderzoekDtos);
  }


  [HttpGet]
  [Route("{id}")]
  public async Task<ActionResult> GetById(Guid id) {
    var onderzoek = await _onderzoekRepository.GetByIdAsync(id);
    if (onderzoek == null) {
      return NotFound();
    }

    var onderzoekDto = _mapper.Map<OnderzoekDto>(onderzoek);
    return Ok(onderzoekDto);
  }

  [HttpPost]
  [Route("create")]
  public async Task<ActionResult<OnderzoekDto>> Create([FromBody] AddOnderzoekRequestDto addDto) {
    var onderzoek = _mapper.Map<Onderzoek>(addDto);
    onderzoek = await _onderzoekRepository.CreateAsync(onderzoek);
    var onderzoekDto = _mapper.Map<OnderzoekDto>(onderzoek);
    return CreatedAtAction(nameof(GetById), new { id = onderzoekDto.Id }, onderzoekDto);
  }


  [HttpPut]
  [Route("update/{id}")]
  public async Task<IActionResult> Update(Guid id, [FromBody] UpdateOnderzoekRequestDto request) {
    try {
      Onderzoek? bestaandOnderzoek = await _onderzoekRepository.GetByIdAsync(id);

      if (bestaandOnderzoek == null) {
        return NotFound($"Onderzoek met ID {id} is niet gevonden.");
      }

      _mapper.Map(request, bestaandOnderzoek);

      Onderzoek? isUpdated = await _onderzoekRepository.UpdateAsync(id, bestaandOnderzoek);

      if (isUpdated == null) {

        return StatusCode(StatusCodes.Status500InternalServerError, "Er is een fout opgetreden bij het bijwerken van het onderzoek.");

      }

      return Ok("Onderzoek succesvol geupdate.");
    } catch (Exception ex) {
      var errorMsg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
      return StatusCode(StatusCodes.Status500InternalServerError, $"Interne serverfout: {errorMsg}");
    }
  }

  [HttpDelete]
  [Route("delete/{id}")]
  public async Task<IActionResult> Delete(Guid id) {
    var success = await _onderzoekRepository.DeleteAsync(id);
    if (!success) {
      return NotFound();
    }

    return Ok("Onderzoek is verwijderd.");
  }

}
