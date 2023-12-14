using Api.Models.Domain;
using Api.Models.DTO;
using Api.Models.DTO.Auth;
using Api.Services.ITokenService;
using Api.Services.IUserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<Gebruiker> gebruikerManager;
    private readonly IUserService userService;
    private readonly ITokenService tokenService;

    public AuthController(UserManager<Gebruiker> gebruikerManager, IUserService userService, ITokenService tokenService)
    {
        this.gebruikerManager = gebruikerManager;
        this.userService = userService;
        this.tokenService = tokenService;
    }

    [HttpPost]
    [Route("Register")]
    [Authorize(Roles = "Beheerder")]
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

    
    [HttpPost]
    [Route("RegisterErvaringsdeskundige")]
    public async Task<IActionResult> RegisterErvaringsdeskundige([FromBody] RegisterErvaringsdeskundigeRequestDto registerErvaringsdeskundigeRequestDto)
    {
        var gebruiker = new Ervaringsdeskundige
        {
            Voornaam = registerErvaringsdeskundigeRequestDto.Voornaam,
            Achternaam = registerErvaringsdeskundigeRequestDto.Achternaam,
            GoogleAccount = registerErvaringsdeskundigeRequestDto.GoogleAccount ?? false,
            Email = registerErvaringsdeskundigeRequestDto.Email,
            UserName = registerErvaringsdeskundigeRequestDto.Email,
            Postcode = registerErvaringsdeskundigeRequestDto.Postcode,
            ToestemmingBenadering = registerErvaringsdeskundigeRequestDto.ToestemmingBenadering,
            Leeftijdscategorie = registerErvaringsdeskundigeRequestDto.Leeftijdscategorie
        };

        string[] Roles = { "Ervaringsdeskundige" };
             

        string result = await userService.Register(gebruiker, registerErvaringsdeskundigeRequestDto.Password, Roles);
        return result.StartsWith("OK") ? Ok(result) : BadRequest(result);
    }

    [HttpPost]
    [Route("RegisterBedrijf")]
    public async Task<IActionResult> RegisterBedrijf([FromBody] RegisterBedrijfRequestDto registerBedrijfRequestDto)
    {
        var gebruiker = new Bedrijf
        {
            Voornaam = registerBedrijfRequestDto.Voornaam,
            Achternaam = registerBedrijfRequestDto.Achternaam,
            GoogleAccount = registerBedrijfRequestDto.GoogleAccount ?? false,
            Email = registerBedrijfRequestDto.Email,
            UserName = registerBedrijfRequestDto.Email,
            Postcode = registerBedrijfRequestDto.Postcode,
            NaamBedrijf = registerBedrijfRequestDto.NaamBedrijf,
            Plaats = registerBedrijfRequestDto.Plaats,
            Nummer = registerBedrijfRequestDto.Nummer,
            WebsiteUrl = registerBedrijfRequestDto.WebsiteUrl,
            Omschrijving = registerBedrijfRequestDto.Omschrijving
        };

        string[] Roles = { "Bedrijf" };

        string result = await userService.Register(gebruiker, registerBedrijfRequestDto.Password, Roles);
        return result.StartsWith("OK") ? Ok(result) : BadRequest(result);
    }

    [HttpPost]
    [Route("RegisterMedwerker")]
    public async Task<IActionResult> RegisterMedewerker([FromBody] RegisterMedewerkerRequestDto registerMedewerkerRequestDto)
    {
        var gebruiker = new Medewerker
        {
            Voornaam = registerMedewerkerRequestDto.Voornaam,
            Achternaam = registerMedewerkerRequestDto.Achternaam,
            GoogleAccount = registerMedewerkerRequestDto.GoogleAccount ?? false,
            Email = registerMedewerkerRequestDto.Email,
            UserName = registerMedewerkerRequestDto.Email,
            Functie = registerMedewerkerRequestDto.Functie
        };

        string[] Roles = { "Medewerker" };

        string result = await userService.Register(gebruiker, registerMedewerkerRequestDto.Password, Roles);
        return result.StartsWith("OK") ? Ok(result) : BadRequest(result);
    }

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
    {

        var gebruiker = await gebruikerManager.FindByEmailAsync(loginRequestDto.Email);
        if (gebruiker == null) { return BadRequest("Ongeldig wachtwoord of emailadres."); }

        var result = await gebruikerManager.CheckPasswordAsync(gebruiker, loginRequestDto.Password);

        if (!result) { return BadRequest("Ongeldig wachtwoord of emailadres."); }

        var roles = await gebruikerManager.GetRolesAsync(gebruiker);
        if (roles == null) { return BadRequest("Ongeldig wachtwoord of emailadres."); }

        var jwtToken = tokenService.CreateJWTToken(gebruiker, roles.ToList());
        return Ok(jwtToken);
    }


    [HttpGet]
    [Authorize]
    public async Task<IActionResult> test()
    {
        var userName = User?.FindFirstValue(ClaimTypes.Email);

        return Ok(userName);
    }
}

