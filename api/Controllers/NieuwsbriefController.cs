using Api.Data;
using Api.Models.Domain.News;
using Api.Models.Domain.User;
using Api.Models.DTO.Nieuwsbrief;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers; 

[Route("[controller]")]
[ApiController]
public class NieuwsbriefController : ControllerBase {

  private readonly AccessibilityDbContext _dbContext;
  private readonly IMapper _mapper;

  public NieuwsbriefController(AccessibilityDbContext dbContext, IMapper mapper) {
    _dbContext = dbContext;
    _mapper = mapper;
  }
  
  [HttpGet]
  public async Task<IActionResult> GetAll() {
    var nieuws = await _dbContext.Nieuws.ToListAsync();
    return Ok(nieuws);
  }

  [HttpPost]
  [Authorize(Roles = "Medewerker")]
  public async Task<IActionResult> CreateNieuwsBrief([FromBody] CreateNiewsbriefDto request) {
    
    var brief = _mapper.Map<Nieuwsbrief>(request);
    await _dbContext.Nieuws.AddAsync(brief);
    var result = await _dbContext.SaveChangesAsync();
    return Ok("Succesfully created the news message.");
  }
  
  [HttpDelete]
  public async Task<IActionResult> DeleteNieuwsbrief([FromBody] Guid id) {
    var bericht = await _dbContext.Nieuws.FindAsync(id);
    if (bericht == null) return NotFound();
     _dbContext.Nieuws.Remove(bericht);
     var result = await _dbContext.SaveChangesAsync();

     if (result != 1) return Problem("Could not delete news article.");
    return Ok(result);
  }

}
