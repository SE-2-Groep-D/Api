﻿using Api.Models.Domain.Research;
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

    var dto = _mapper.Map<QuestionListDto>(list);
    return Ok(dto);
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
  
  [HttpDelete("{id}")]
  public async Task<ActionResult> Delete([FromRoute] Guid id) {
    var list = await _vragenlijstRepository.DeleteAsync(id);
    if (!list) {
      return new StatusCodeResult(500);
    }
    
    return Ok();
  }
  
}