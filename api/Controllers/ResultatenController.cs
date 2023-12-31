using Api.Models.DTO.Onderzoek;
using Api.Repositories;
using Api.Repositories.ITrackingRepository;
using Api.Repositories.VragenlijstRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers; 


[Route("[controller]")]
[ApiController]
public class ResultatenController : ControllerBase {

  private readonly IMapper _mapper;
  private readonly ITrackingRepository _trackingRepository;
  private readonly IVragenlijstRepository _onderzoekRepository;
  
  public ResultatenController(IMapper mapper, ITrackingRepository trackingRepository, IVragenlijstRepository onderzoekRepository) {
    _mapper = mapper;
    _trackingRepository = trackingRepository;
    _onderzoekRepository = onderzoekRepository;
  }
  
  
  [HttpGet("{id}")]
  public async Task<IActionResult> GetResults(Guid id) {
    var vragenlijsten = await _onderzoekRepository.GetAllAsync(id);
    var vragenlijstenDtos = _mapper.Map<IEnumerable<VragenlijstDto>>(vragenlijsten);
    
    var resultaat = await _trackingRepository.GetById(id);

    var results = new {
      tracking = resultaat,
      vragenlijst = vragenlijstenDtos
    };
    
    return Ok(results);
  }
  

}
