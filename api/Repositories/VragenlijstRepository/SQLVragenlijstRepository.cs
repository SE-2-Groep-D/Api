using System.Text.Json;
using System.Text.Json.Serialization;
using Api.Data;
using Api.Models.Domain.Research;
using Api.Models.DTO.Onderzoek;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories.VragenlijstRepository;
public class SQLVragenlijstRepository : IVragenlijstRepository {

  private AccessibilityDbContext _context;
  private readonly IMapper _mapper;

  public SQLVragenlijstRepository(AccessibilityDbContext context, IMapper mapper) {
    _context = context;
    _mapper = mapper;
  }

  public async Task<List<Vragenlijst>> GetAllAsync(Guid OnderzoekId) {
    return await _context.Vragenlijsten.Where(v => v.OnderzoekId == OnderzoekId).ToListAsync();
  }

  public async Task<VragenlijstDto?> GetByIdAsync(Guid id) {
    var vragenlijst = _context.Vragenlijsten
      .Include(v => v.Vragen)
      .ThenInclude(v => v.Antwoorden)
      .SingleOrDefault(v => v.Id == id);

    if (vragenlijst == null) {
      return null;
    }

    var vragenlijstDTO = _mapper.Map<VragenlijstDto>(vragenlijst);
    await AddResearchInfo(vragenlijst, vragenlijstDTO);
    return vragenlijstDTO;
  }
  
  public async Task AddResearchInfo(Vragenlijst trackingOnderzoek, VragenlijstDto dto) {
    var onderzoek = await _context.Onderzoeken.FindAsync(trackingOnderzoek.OnderzoekId);
    dto.Participants = (onderzoek == null) ? 0 : onderzoek.AantalParticipanten;
    dto.TotalQuestions = (int)trackingOnderzoek.Vragen.Count();
    dto.TotalAwnsers = trackingOnderzoek.Vragen.Sum(vraag => vraag.Antwoorden.Count());

    // dto.TimePerPage = (int)trackingOnderzoek.TrackingResultaten
    //   .Select(resultaten => resultaten.TimeInSeconds)
    //   .DefaultIfEmpty(0) 
    //   .Average() / 60;
  }

  public async Task<Vragenlijst> CreateAsync(Vragenlijst vragenlijst) {
    await _context.Vragenlijsten.AddAsync(vragenlijst);
    await _context.SaveChangesAsync();
    return vragenlijst;

  }

  public async Task<Vragenlijst?> UpdateAsync(Guid id, VragenlijstDto vragenlijst) {
    var bestaandVragenlijst = await _context.Vragenlijsten.FindAsync(id);
    _context.Entry(bestaandVragenlijst).CurrentValues.SetValues(vragenlijst);
    await _context.SaveChangesAsync();
    return bestaandVragenlijst;
  }

  public async Task<bool> DeleteAsync(Guid id) {

    var vragenlijst = await _context.Vragenlijsten.FindAsync(id);
    if (vragenlijst == null) {
      return false;
    }

    _context.Vragenlijsten.Remove(vragenlijst);
    await _context.SaveChangesAsync();
    return true;

  }



}