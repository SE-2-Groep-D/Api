using Api.Models.Domain.Research;
using Api.Models.DTO.Onderzoek;
using Api.Repositories.VragenlijstRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Enum = Google.Protobuf.WellKnownTypes.Enum;
using System;
using Api.Models.Domain.Research.Questionlist;
using Api.Models.DTO.Onderzoek.request;
using Api.Models.DTO.Onderzoek.response;

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
  public async Task<ActionResult> GetAll(Guid onderzoekId) {
    var vragenlijsten = await _vragenlijstRepository.GetAllAsync(onderzoekId);
    var vragenlijstDtos = _mapper.Map<List<MinimalQuestionListDto>>(vragenlijsten);
    return Ok(vragenlijstDtos);
  }
  
  [HttpGet("{id}")]
  public async Task<ActionResult> GetQuestionList([FromRoute] Guid id) {
    var list = await _vragenlijstRepository.GetByIdAsync(id);
    var dto = _mapper.Map<QuestionListDto>(list);
    return Ok(dto);
  }
  
  [HttpPost]
  public async Task<ActionResult> Create(CreateQuestionListDto questionListDto) {
    var list = await _vragenlijstRepository.CreateAsync(questionListDto);
    if (list == null) {
      return new BadRequestResult();
    }

    var response = _mapper.Map<QuestionListDto>(list);
    return Ok(response);
  }
  
  [HttpPut("{id}")]
  public async Task<ActionResult> Create([FromRoute] Guid id, UpdateQuestionListDto questionListDto) {
    var list = await _vragenlijstRepository.UpdateAsync(id, questionListDto);
    if (list == null) {
      return new BadRequestResult();
    }

    var response = _mapper.Map<QuestionListDto>(list);
    return Ok(response);
  }

  // [HttpGet]
  // [Route("{id}")]
  // public async Task<ActionResult> GetById(Guid id) {
  //   var vragenlijsten = await _vragenlijstRepository.GetByIdAsync(id);
  //   if (vragenlijsten == null) return NotFound();
  //   var vragenlijstenDto = _mapper.Map<QuestionListDto>(vragenlijsten);
  //   return Ok(vragenlijstenDto);
  // }
  //
  // [HttpPost]
  // [Route("create")]
  // public async Task<IActionResult> Create([FromBody] CreateQuestionListDto addDto) {
  //   var questionlist = _mapper.Map<QuestionList>(addDto);
  //   questionlist = await _vragenlijstRepository.CreateAsync(questionlist);
  //   var questionlistDto = _mapper.Map<QuestionListDto>(questionlist);
  //   return Ok(questionlistDto);
  // }
  //
  //
  // [HttpPut]
  // [Route("update/{id}")]
  // public async Task<IActionResult> Update(Guid id, [FromBody] UpdateQuestionlistRequestDto requestDto) {
  //   var questionlist = await _vragenlijstRepository.GetByIdAsync(id);
  //   if (questionlist == null) {
  //     return NotFound();
  //   }
  //   
  //   try {
  //     return Ok(_vragenlijstRepository.UpdateAsync(id, requestDto));
  //   } catch (Exception ex) {
  //     return StatusCode(StatusCodes.Status500InternalServerError);
  //   }
  // }
  //
  //
  // [HttpDelete]
  // [Route("delete/{id}")]
  // public async Task<IActionResult> Delete(Guid id) {
  //   var success = await _vragenlijstRepository.DeleteAsync(id);
  //   if (!success) {
  //     return NotFound();
  //   }
  //
  //   return Ok("Vragenlijst is succsesvol updated");
  // }

}