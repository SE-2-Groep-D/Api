using Api.Models.Domain;

namespace Api.Repositories; 

public interface IGebruikerRepository {

    public Task<List<Gebruiker>> GetAllAsync();
}