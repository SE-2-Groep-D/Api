using Api.Models.Domain;
using Microsoft.AspNetCore.Identity;

namespace Api.Services.ITokenService
{
    public interface ITokenService
    {
        string CreateJWTToken(Gebruiker user, List<string> roles);
    }
}
