using Api.Models.Domain;
using Api.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("[controller]")]
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
            GoogleAccount = registerRequestDto.GoogleAccount ?? false,
            Email = registerRequestDto.Email,
            UserName = registerRequestDto.Email
        };


        var identityResult = await gebruikerManager.CreateAsync(identityGebruiker, registerRequestDto.Password);

        if(identityResult.Succeeded)
        {
            identityResult = await gebruikerManager.AddToRolesAsync(identityGebruiker, registerRequestDto.Roles);
                
            if (identityResult.Succeeded)
            {
                return Ok("User was registerd! Please Login.");
            }
        }  
        else 
        { 
            await gebruikerManager.DeleteAsync(identityGebruiker);
            return BadRequest("Ongeldige rol");
        }



        return BadRequest("Someting went wrong!");
    }
}

