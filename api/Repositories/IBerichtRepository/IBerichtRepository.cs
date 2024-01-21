namespace Api.Repositories.IBerichtRepository {

  using Api.Models.Domain.Bericht;
  using Api.Models.DTO.Bericht;

  public interface IBerichtRepository {

    public Task<Bericht> CreateBericht(Bericht bericht);

    Task<IEnumerable<ChatResponseDto>> GetChatsByUserId(Guid userId);

    Task<IEnumerable<Bericht>> GetBerichten(Guid verzenderId, Guid ontvangerId);
    public Task<string> GetNaam(string userId);
  }
}
