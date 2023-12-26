namespace Api.Repositories.IBerichtRepository {

  using Api.Models.Domain.Bericht;

  public interface IBerichtRepository {

    public Task<Bericht> CreateBericht(Bericht bericht);

    Task<IEnumerable<Bericht>> GetChatsByUserId(Guid userId);

  }
}
