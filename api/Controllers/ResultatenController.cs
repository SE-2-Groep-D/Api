using Api.Models.DTO.Onderzoek.response;
using Api.Models.DTO.Onderzoek.results;
using Api.Repositories.ITrackingRepository;
using Api.Repositories.VragenlijstRepository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
[Route("[controller]")]
[ApiController]
[Authorize]
public class ResultatenController : ControllerBase {

  private readonly IMapper _mapper;
  private readonly IVragenlijstRepository _onderzoekRepository;
  private readonly ITrackingRepository _trackingRepository;

  public ResultatenController(IMapper mapper, ITrackingRepository trackingRepository, IVragenlijstRepository onderzoekRepository) {
    _mapper = mapper;
    _trackingRepository = trackingRepository;
    _onderzoekRepository = onderzoekRepository;
  }


  [HttpGet("{id}")]
  public async Task<IActionResult> GetResults(Guid id) {
    var questionLists = await _onderzoekRepository.GetAllAsync(id);
    var trackingResearch = await _trackingRepository.GetTrackingOnderzoeken(id);

    var mappedQuestionLists = _mapper.Map<List<ResponseQuestionListDto>>(questionLists);
    var mappedTrackingResearch = _mapper.Map<List<ResponseTrackingDto>>(trackingResearch);

    return Ok(new ResultResponseDto {
      QuestionList = mappedQuestionLists,
      TrackingResearches = mappedTrackingResearch
    });
  }

}
