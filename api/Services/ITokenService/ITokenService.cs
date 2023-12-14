using Microsoft.AspNetCore.Identity;

namespace Api.Services.ITokenService
{
    public interface ITokenService
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
