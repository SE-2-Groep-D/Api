﻿using Api.Models.Domain.Research.Tracking;
using Api.Models.DTO.Onderzoek.tracking;
using Api.Repositories.ITrackingRepository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Ocsp;

namespace Api.Controllers;
[Route("[controller]")]
[ApiController]
public class TrackingController : ControllerBase {

  private readonly IMapper _mapper;
  private readonly ITrackingRepository _repository;
  
  public TrackingController(IMapper mapper, ITrackingRepository repository) {
    _mapper = mapper;
    _repository = repository;
  }

  [HttpGet]
  public IActionResult GetJavaScriptFile() {
    var jsFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Public", "trackingscript.js");
    return PhysicalFile(jsFilePath, "text/javascript");
  }

  [HttpGet("script")]
  public IActionResult GetScriptTag() {
    var scriptUrl = Url.Action("GetJavaScriptFile", "Tracking", null, Request.Scheme);
    var scriptTag = $"<script id=\"tracking-script\" src=\"{scriptUrl}\" defer></script>";
    return Ok(scriptTag);
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> GetResults(Guid id) {
    var resultaat = await _repository.GetById(id);
    if (resultaat == null) return NotFound();
    return Ok(resultaat);
  }

  [HttpPost]
  public async Task<IActionResult> SubmitResults([FromBody] SubmitTrackingResultsDto request) {
    bool submitted = await _repository.SubmitResults(request);
    if (!submitted) return BadRequest();
    return Ok(request);
  }

  [HttpPost("create")]
  // [Authorize(Roles = "Bedrijf")]
  public async Task<IActionResult> CreateTrackingResearch([FromBody] CreateTrackingResearchDto request) {
    bool created = await _repository.CreateTrackingReasearch(request);
    if (!created) return NotFound();
    return Ok();
  }

  [HttpGet("{id}")] public async Task<IActionResult> DeleteResults(Guid id) {
    var resultaat = await _repository.DeleteAsync(id);
    if (!resultaat) return BadRequest();
    return Ok(resultaat);
  }
}