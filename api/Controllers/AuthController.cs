using Api.Models.Domain;
using Api.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

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
            var gebruiker = new Gebruiker
            {
                Voornaam = registerRequestDto.Voornaam,
                Achternaam = registerRequestDto.Achternaam,
                GoogleAccount = registerRequestDto.GoogleAccount,
                Email = registerRequestDto.Email
            };

            return BadRequest("Someting went wrong!");
        }
    }

