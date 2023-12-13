using Api.Models.Domain;
using Api.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<Gebruiker> gebruikerManager;

        public AuthController(UserManager<Gebruiker> gebruikerManager)
        {
            this.gebruikerManager = gebruikerManager;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {

            if (registerRequestDto.Roles == null || !registerRequestDto.Roles.Any())
            {
                return BadRequest("Geef rol aan");
            }

            var identityGebruiker = new Gebruiker
            {
                Voornaam = registerRequestDto.Voornaam,
                Achternaam = registerRequestDto.Achternaam,
                GoogleAccount = registerRequestDto.GoogleAccount,
                Email = registerRequestDto.Email
            };

            

            var identityResult = await gebruikerManager.CreateAsync(identityGebruiker, registerRequestDto.Password);

            if(identityResult.Succeeded)
            {
                identityResult = await gebruikerManager.
            }

            return BadRequest("Someting went wrong!");
        }
    }
}
