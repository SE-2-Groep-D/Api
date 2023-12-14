using Api.Models.Domain;
using Api.Models.DTO;
using Microsoft.AspNetCore.Identity;

namespace Api.Services.IUserService
{
    public class UserService : IUserService
    {
        private readonly UserManager<Gebruiker> gebruikerManager;

        public UserService(UserManager<Gebruiker> gebruikerManager)
        {
            this.gebruikerManager = gebruikerManager;
        }

        public async Task<string> Register(Gebruiker gebruiker, string password, string[] roles)
        {
            if (roles == null || !roles.Any())
            {
                return "Geef rol aan";
            }

            var identityResult = await gebruikerManager.CreateAsync(gebruiker, password);

            if (identityResult.Succeeded)
            {
                identityResult = await gebruikerManager.AddToRolesAsync(gebruiker, roles);
            }
            else
            {
                return "Er ging iets mis!";
            }

            if (identityResult.Succeeded)
            {
                return "OK: User was registerd! Please Login.";
            }
            else
            {
                await gebruikerManager.DeleteAsync(gebruiker);
                return "Ongeldige rol";
            }

            
        }
    }
}
