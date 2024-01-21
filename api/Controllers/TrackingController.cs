using Api.Models.DTO.Onderzoek.tracking;
using Api.Repositories.ITrackingRepository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

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
  [EnableCors("AllowAnyOrigin")]
  public IActionResult GetJavaScriptFile() {
    var jsFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Public", "trackingscript.js");
    return PhysicalFile(jsFilePath, "text/javascript");
  }

  [HttpGet("script")]
  [EnableCors("AllowAnyOrigin")]
  public IActionResult GetScriptTag() {
    var scriptUrl = Url.Action("GetJavaScriptFile", "Tracking", null, Request.Scheme);
    var scriptTag = $"<script id=\"tracking-script\" src=\"{scriptUrl}\" defer></script>";
    return Ok(scriptTag);
  }

  [HttpGet("{id}")]
  [Authorize]
  public async Task<IActionResult> GetResults(Guid id) {
    var resultaat = await _repository.GetById(id);
    if (resultaat == null) return NotFound();
    return Ok(resultaat);
  }

  [HttpPost]
  [EnableCors("AllowAnyOrigin")]
  public async Task<IActionResult> SubmitResults([FromBody] SubmitTrackingResultsDto request) {
    var submitted = await _repository.SubmitResults(request);
    if (!submitted) return NotFound();
    return Ok(request);
  }

  [HttpPost("create")]
  [Authorize]
  [Authorize(Roles = "Bedrijf,Beheerder")]
  public async Task<IActionResult> CreateTrackingResearch([FromBody] CreateTrackingResearchDto request) {
    var created = await _repository.CreateTrackingResearch(request);
    if (!created) return NotFound();
    return Ok();
  }

  [HttpDelete("{id}")]
  [Authorize]
  [Authorize(Roles = "Bedrijf,Beheerder")]
  public async Task<IActionResult> DeleteResearch(Guid id) {
    var resultaat = await _repository.DeleteTrackingResearch(id);
    if (!resultaat) return BadRequest();
    return Ok(resultaat);
  }

  [HttpPut("{id}")]
  [Authorize(Roles = "Bedrijf,Beheerder")]
  [Authorize]
  public async Task<IActionResult> UpdateResearch(UpdateTrackingResearchDto request) {
    var resultaat = await _repository.UpdateTrackingResearch(request);
    if (!resultaat) return BadRequest();
    return Ok(resultaat);
  }

}
