using Api.Data;
using Api.Models.Domain.Research.Tracking;
using Api.Models.DTO.Onderzoek.results;
using Api.Models.DTO.Onderzoek.tracking;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories.ITrackingRepository;
public class TrackingRepository : ITrackingRepository {

  private readonly AccessibilityDbContext _context;
  private readonly IMapper _mapper;

  private readonly IOnderzoekRepository _onderzoekRepository;

  public TrackingRepository(AccessibilityDbContext context, IOnderzoekRepository repository, IMapper mapper) {
    _context = context;
    _onderzoekRepository = repository;
    _mapper = mapper;
  }

  public async Task<bool> CreateTrackingResearch(CreateTrackingResearchDto request) {
    var trackingOnderzoek = _mapper.Map<TrackingOnderzoek>(request);
    if (trackingOnderzoek == null) return false;
    var onderzoek = await _onderzoekRepository.GetByIdAsync(trackingOnderzoek.OnderzoekId);
    if (onderzoek == null) return false;
    await _context.TrackingOnderzoeken.AddAsync(trackingOnderzoek);
    var result = await _context.SaveChangesAsync();
    return true;
  }

  public async Task<bool> SubmitResults(SubmitTrackingResultsDto results) {
    var onderzoek = await _context.TrackingOnderzoeken.FirstOrDefaultAsync(trackingOnderzoek => new Uri(trackingOnderzoek.Domain).Host.ToLower().Equals(results.Domain.ToLower()));
    if (onderzoek == null) return false;
    var trackingResult = _mapper.Map<TrackingResultaten>(results);
    onderzoek.TrackingResultaten.Add(trackingResult);
    await _context.SaveChangesAsync();
    return true;
  }

  public async Task<List<TrackingOnderzoek>> GetTrackingOnderzoeken(Guid id) {
    return await _context.TrackingOnderzoeken.Where(onderzoek => onderzoek.OnderzoekId == id).ToListAsync();
  }

  public async Task<ResponseTrackingDto?> GetById(Guid id) {
    var onderzoek = await _context.TrackingOnderzoeken
      .Include(o => o.TrackingResultaten)
      .FirstOrDefaultAsync(trackingOnderzoek => trackingOnderzoek.Id == id);

    if (onderzoek == null) {
      return null;
    }

    var dto = _mapper.Map<ResponseTrackingDto>(onderzoek);
    await AddResearchInfo(onderzoek, dto);
    await AddOtherResults(onderzoek, dto);
    return dto;
  }




  public async Task<bool> DeleteTrackingResearch(Guid onderzoekId) {
    var result = await GetById(onderzoekId);
    if (result == null) return true;
    _context.Remove(result);
    await _context.SaveChangesAsync();
    return true;
  }

  public async Task<bool> UpdateTrackingResearch(UpdateTrackingResearchDto request) {
    var onderzoek = await GetById(request.OnderzoekId);
    if (onderzoek == null) return false;
    var map = _mapper.Map(request, onderzoek);
    var result = await _context.TrackingOnderzoeken.FindAsync(map.Id);
    if (result == null) return false;

    _context.Entry(result).CurrentValues.SetValues(map);
    await _context.SaveChangesAsync();
    return true;
  }

  public async Task AddResearchInfo(TrackingOnderzoek trackingOnderzoek, ResponseTrackingDto dto) {
    var onderzoek = await _context.Onderzoeken.FindAsync(trackingOnderzoek.OnderzoekId);
    dto.Participants = onderzoek == null ? 0 : onderzoek.AantalParticipanten;
    dto.ScrollPercentage = (int)trackingOnderzoek.TrackingResultaten
      .Select(resultaten => resultaten.PagePercentage)
      .DefaultIfEmpty(0)
      .Average();

    dto.TimePerPage = (int)trackingOnderzoek.TrackingResultaten
      .Select(resultaten => resultaten.TimeInSeconds)
      .DefaultIfEmpty(0)
      .Average() / 60;
  }

  public async Task AddOtherResults(TrackingOnderzoek trackingOnderzoek, ResponseTrackingDto dto) {
    var mostUsedBrowser = trackingOnderzoek.TrackingResultaten.GroupBy(resultaten => resultaten.Browser)
      .OrderByDescending(group => group.Count()).Select(g => g.Key).FirstOrDefault();

    var mostVisitedPage = trackingOnderzoek.TrackingResultaten.GroupBy(resultaten => resultaten.Page)
      .OrderByDescending(group => group.Count()).Select(g => g.Key).FirstOrDefault();

    var mostClickedLink = trackingOnderzoek.TrackingResultaten
      .SelectMany(resultaten => resultaten.ClickedItems)
      .GroupBy(clickedItem => clickedItem.Href)
      .OrderByDescending(group => group.Count())
      .Select(group => group.Key)
      .FirstOrDefault();

    dto.OtherResults = new List<OtherResult> {
      new() {
        Title = "Meest gebruikte browser",
        Value = mostUsedBrowser
      },
      new() {
        Title = "Meest bezochte pagina",
        Value = mostVisitedPage
      },
      new() {
        Title = "Meest geklikte link",
        Value = mostClickedLink
      }
    };
  }

}
