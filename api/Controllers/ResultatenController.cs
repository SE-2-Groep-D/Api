using Api.Models.DTO.Onderzoek;
using Api.Models.DTO.Onderzoek.results;
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
    var questionLists = await _onderzoekRepository.GetAllAsync(id);
    var trackingResearch = await _trackingRepository.GetTrackingOnderzoeken(id);
    
    var mappedQuestionLists = _mapper.Map<List<ResponseVragenlijstDto>>(questionLists);
    var mappedTrackingResearch = _mapper.Map<List<ResponseTrackingDto>>(trackingResearch);

    
    return Ok(new ResultResponseDto {
      Vragenlijsten = mappedQuestionLists,
      TrackingOnderzoeken = mappedTrackingResearch,
    });
  }
  

}
