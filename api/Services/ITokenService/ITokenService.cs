using Api.Models.Domain;
using Api.Models.Domain.User;
using Microsoft.AspNetCore.Identity;

namespace Api.Services.ITokenService {

  public interface ITokenService {

    string CreateJWTToken(Gebruiker user, List<string> roles);

  }

}
