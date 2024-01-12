using Api.Models.Domain.Research.Tracking;
using Api.Models.DTO.Onderzoek.results;
using Api.Models.DTO.Onderzoek.tracking;

namespace Api.Repositories.ITrackingRepository;
public interface ITrackingRepository {

  public Task<bool> CreateTrackingResearch(CreateTrackingResearchDto request);
  public Task<bool> SubmitResults(SubmitTrackingResultsDto results);

  public Task<List<TrackingOnderzoek>> GetTrackingOnderzoeken(Guid id);
  public Task<ResponseTrackingDto?> GetById(Guid onderzoekId);

  public Task<bool> DeleteTrackingResearch(Guid onderzoekId);
  public Task<bool> UpdateTrackingResearch(UpdateTrackingResearchDto request);

}
