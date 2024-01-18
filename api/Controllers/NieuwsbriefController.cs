using Api.Data;
using Api.Models.DTO.Nieuwsbrief;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;
[Route("[controller]")]
[ApiController]
[Authorize]
public class NieuwsbriefController : ControllerBase {

  private readonly AccessibilityDbContext _dbContext;
  private readonly IMapper _mapper;

  public NieuwsbriefController(AccessibilityDbContext dbContext, IMapper mapper) {
    _dbContext = dbContext;
    _mapper = mapper;
  }

  [HttpGet]
  public async Task<IActionResult> GetAll() {
    var last30Days = DateTime.UtcNow.AddDays(-30);

    var newsArticles = await _dbContext.Nieuws
      .Where(n => n.Datum >= last30Days)
      .Include(nieuwsbrief => nieuwsbrief.Medewerker) // Include Medewerker navigation property
      .ToListAsync();
    
    var newsArticlesDto = _mapper.Map<IEnumerable<Nieuwsbrief>, IEnumerable<NieuwsBriefDto>>(newsArticles);
    return Ok(newsArticlesDto);
  }

  [HttpPost]
  [Authorize(Roles = "Medewerker,Beheerder")]
  public async Task<IActionResult> CreateNieuwsBrief([FromBody] CreateNiewsbriefDto request) {
    try {
      var brief = _mapper.Map<Nieuwsbrief>(request);
      await _dbContext.Nieuws.AddAsync(brief);
      var result = await _dbContext.SaveChangesAsync();
      return Ok(brief.Id);
    } catch (Exception ex) {
      Console.WriteLine(ex);
      return Problem("Could not create news article");
    }
  }

  
  [HttpPut("update/{id}")]
  [Authorize(Roles = "Medewerker,Beheerder")]
  public async Task<IActionResult> UpdateNieuwsbrief([FromRoute] Guid id, [FromBody] UpdateNieuwsbriefDto request) {
    var nieuwsbrief = await _dbContext.Nieuws.FindAsync(id);
    if (nieuwsbrief == null) {
      return NotFound();
    }

    _mapper.Map(request, nieuwsbrief);
    await _dbContext.SaveChangesAsync();
    return Ok(nieuwsbrief);
  }

  [HttpDelete("delete/{id}")]
  [Authorize(Roles = "Medewerker,Beheerder")]
  public async Task<IActionResult> DeleteNieuwsbrief([FromRoute] Guid id) {
    var bericht = await _dbContext.Nieuws.FindAsync(id);
    if (bericht == null) return NotFound();
    _dbContext.Nieuws.Remove(bericht);
    var result = await _dbContext.SaveChangesAsync();

    if (result != 1) return Problem("Could not delete news article.");
    return Ok(result);
  }

}
