using Api.Models.Domain;
using Microsoft.AspNetCore.Identity;

namespace Api.Services.IUserService;

public class UserService : IUserService {
    
        private readonly UserManager<Gebruiker> gebruikerManager;

        public UserService(UserManager<Gebruiker> gebruikerManager) {
            this.gebruikerManager = gebruikerManager;
        }

        public async Task<string> Register(Gebruiker gebruiker, string password, string[] roles) {
            if (!roles.Any()) {
                return "Geef rol aan";
            }

            var identityResult = await gebruikerManager.CreateAsync(gebruiker, password);
            if (!identityResult.Succeeded) {
                return "Er ging iets mis!";
            }
            
            identityResult = await gebruikerManager.AddToRolesAsync(gebruiker, roles);
            if (!identityResult.Succeeded) {
                await gebruikerManager.DeleteAsync(gebruiker);
                return "Ongeldige rol";
            }
            
            return "OK: User was registerd! Please Login.";
        }
}

