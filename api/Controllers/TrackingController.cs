using Api.Models.DTO.Onderzoek.tracking;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Ocsp;

namespace Api.Controllers;
[Route("[controller]")]
[ApiController]
public class TrackingController : ControllerBase {

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

  [HttpPost]
  public async Task<IActionResult> SubmitResults([FromBody] SubmitTrackingResultsDto request) {
    
    
    return Ok(request);
  }
}