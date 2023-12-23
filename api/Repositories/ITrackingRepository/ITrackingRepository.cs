using Api.Models.Domain.Research.Tracking;
using Api.Models.DTO.Onderzoek.tracking;

namespace Api.Repositories.ITrackingRepository;
public interface ITrackingRepository {

  public Task<bool> CreateTrackingResearch(CreateTrackingResearchDto request);
  public Task<bool> SubmitResults(SubmitTrackingResultsDto results);

  public Task<List<TrackingOnderzoek>> GetTrackingOnderzoeken();
  public Task<TrackingOnderzoek?> GetById(Guid onderzoekId);

  public Task<bool> DeleteTrackingResearch(Guid onderzoekId);
  public Task<bool> UpdateTrackingResearch(UpdateTrackingResearchDto request);


}
