using Api.Data;
using Api.Models.Domain.Research.Tracking;
using Api.Models.DTO.Onderzoek.tracking;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories.ITrackingRepository; 
public class TrackingRepository : ITrackingRepository {
  
  private readonly IOnderzoekRepository _onderzoekRepository;
  private readonly AccessibilityDbContext _context;
  private readonly IMapper _mapper;

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
    var onderzoek = await _context.TrackingOnderzoeken.FirstOrDefaultAsync(trackingOnderzoek => trackingOnderzoek.Domain.Equals(results.Domain));
    if (onderzoek == null) return false;
    var trackingResult = _mapper.Map<TrackingResultaten>(results);
    onderzoek.TrackingResultaten.Add(trackingResult);
    await _context.SaveChangesAsync();
    return true;
  }

  public async Task<List<TrackingOnderzoek>> GetTrackingOnderzoeken(Guid id) {
    return await _context.TrackingOnderzoeken.Where(onderzoek => onderzoek.OnderzoekId == id).ToListAsync();
  }

  public async Task<TrackingOnderzoek?> GetById(Guid id) {
    return await _context.TrackingOnderzoeken
      .Include(o => o.TrackingResultaten).FirstOrDefaultAsync(trackingOnderzoek => trackingOnderzoek.Id == id);
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

}
