using Api.Models.Domain.User;

namespace Api.Services.ITokenService; 
public interface ITokenService {

  string CreateJWTToken(Gebruiker user, List<string> roles);

}
