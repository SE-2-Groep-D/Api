using Api.Models.DTO.Research;

namespace Api.Repositories.ITrackingRepository; 

public interface ITrackingRepository {

  public Task<bool> SubmitResults(SubmitTrackingResultsDto results);
}