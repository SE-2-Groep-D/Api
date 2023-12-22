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

  public async Task<bool> CreateTrackingReasearch(CreateTrackingResearchDto request) {
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

  public async Task<List<TrackingOnderzoek>> GetTrackingOnderzoeken() {
    return  await _context.TrackingOnderzoeken
      .Include(o => o.TrackingResultaten)
      .ToListAsync();
    ;
  }

  public async Task<TrackingOnderzoek?> GetTrackingResults(Guid onderzoekId) {
    return await _context.TrackingOnderzoeken
      .Include(o => o.TrackingResultaten).FirstOrDefaultAsync(trackingOnderzoek => trackingOnderzoek.OnderzoekId == onderzoekId);;
  }

}
