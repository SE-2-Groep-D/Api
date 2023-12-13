using Api.Models.Domain;
using Api.Models.DTO;
using Api.Models.DTO.Auth;
using Api.Services.IUserService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<Gebruiker> gebruikerManager;
    private readonly UserManager<Ervaringsdeskundige> ervaringsdeskundigeManager;
    private readonly IUserService userService;

    public AuthController(UserManager<Gebruiker> gebruikerManager, UserManager<Ervaringsdeskundige> ervaringsdeskundigeManager, IUserService userService)
    {
        this.gebruikerManager = gebruikerManager;
        this.ervaringsdeskundigeManager = ervaringsdeskundigeManager;
        this.userService = userService;
    }

    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
    {
        
        var gebruiker = new Gebruiker
        {
            Voornaam = registerRequestDto.Voornaam,
            Achternaam = registerRequestDto.Achternaam,
            GoogleAccount = registerRequestDto.GoogleAccount ?? false,
            Email = registerRequestDto.Email,
            UserName = registerRequestDto.Email
        };

        string result = await userService.Register(gebruiker, registerRequestDto.Password, registerRequestDto.Roles);
        return result.StartsWith("OK")? Ok(result) : BadRequest(result);
        
        
    }

    //Nog niet af
    //[HttpPost]
    //[Route("RegisterErvaringsdeskundige")]
    //public async Task<IActionResult> RegisterErvaringsdeskundige([FromBody] RegisterErvaringsdeskundigeRequestDto registerErvaringsdeskundigeRequestDto)
    //{
        

    //    var identityGebruiker = new Ervaringsdeskundige
    //    {
    //        Voornaam = registerErvaringsdeskundigeRequestDto.Voornaam,
    //        Achternaam = registerErvaringsdeskundigeRequestDto.Achternaam,
    //        GoogleAccount = registerErvaringsdeskundigeRequestDto.GoogleAccount ?? false,
    //        Email = registerErvaringsdeskundigeRequestDto.Email,
    //        UserName = registerErvaringsdeskundigeRequestDto.Email,
    //        Postcode = registerErvaringsdeskundigeRequestDto.Postcode,
    //        ToestemmingBenadering = registerErvaringsdeskundigeRequestDto.ToestemmingBenadering,
    //        Leeftijdscategorie = registerErvaringsdeskundigeRequestDto.Leeftijdscategorie
    //    };

    //    string result = await userService.RegisterErvaringsdeskundige(gebruiker, registerRequestDto.Password, registerRequestDto.Roles);
    //    return result.StartsWith("OK") ? Ok(result) : BadRequest(result);
    //}
}

