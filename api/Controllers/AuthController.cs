using Api.Models.Domain;
using Api.Models.DTO;
using Api.Models.DTO.Auth;
using Api.Services.ITokenService;
using Api.Services.IUserService;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Api.Models.Domain.User;

namespace Api.Controllers;
[Route("[controller]")]
[ApiController]
public class AuthController : ControllerBase {

  private readonly UserManager<Gebruiker> gebruikerManager;
  private readonly IUserService userService;
  private readonly ITokenService tokenService;
  private readonly IMapper mapper;

  public AuthController(UserManager<Gebruiker> gebruikerManager, IUserService userService, ITokenService tokenService, IMapper mapper) {
    this.gebruikerManager = gebruikerManager;
    this.userService = userService;
    this.tokenService = tokenService;
    this.mapper = mapper;
  }

  [HttpPost]
  [Route("Register")]
  [Authorize(Roles = "Beheerder")]
  public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto) {
    var gebruiker = mapper.Map<Gebruiker>(registerRequestDto);

    string result = await userService.Register(gebruiker, registerRequestDto.Password, registerRequestDto.Roles);
    return result.StartsWith("OK") ? Ok(result) : BadRequest(result);


  }


  [HttpPost]
  [Route("RegisterErvaringsdeskundige")]
  public async Task<IActionResult> RegisterErvaringsdeskundige([FromBody] RegisterErvaringsdeskundigeRequestDto registerErvaringsdeskundigeRequestDto) {
    var gebruiker = mapper.Map<Ervaringsdeskundige>(registerErvaringsdeskundigeRequestDto);

    string[] Roles = { "Ervaringsdeskundige" };

    string result = await userService.Register(gebruiker, registerErvaringsdeskundigeRequestDto.Password, Roles);
    return result.StartsWith("OK") ? Ok(result) : BadRequest(result);
  }

  [HttpPost]
  [Route("RegisterBedrijf")]
  public async Task<IActionResult> RegisterBedrijf([FromBody] RegisterBedrijfRequestDto registerBedrijfRequestDto) {

    var gebruiker = mapper.Map<Bedrijf>(registerBedrijfRequestDto);


    string[] Roles = { "Bedrijf" };

    string result = await userService.Register(gebruiker, registerBedrijfRequestDto.Password, Roles);
    return result.StartsWith("OK") ? Ok(result) : BadRequest(result);
  }

  [HttpPost]
  [Route("RegisterMedwerker")]
  public async Task<IActionResult> RegisterMedewerker([FromBody] RegisterMedewerkerRequestDto registerMedewerkerRequestDto) {
    var gebruiker = mapper.Map<Medewerker>(registerMedewerkerRequestDto);

    string[] Roles = { "Medewerker" };

    string result = await userService.Register(gebruiker, registerMedewerkerRequestDto.Password, Roles);
    return result.StartsWith("OK") ? Ok(result) : BadRequest(result);
  }

  [HttpPost]
  [Route("Login")]
  public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto) {

    var gebruiker = await gebruikerManager.FindByEmailAsync(loginRequestDto.Email);
    if (gebruiker == null) { return BadRequest("Ongeldig wachtwoord of emailadres."); }

    var result = await gebruikerManager.CheckPasswordAsync(gebruiker, loginRequestDto.Password);

    if (!result) { return BadRequest("Ongeldig wachtwoord of emailadres."); }

    var roles = await gebruikerManager.GetRolesAsync(gebruiker);
    if (roles == null) { return BadRequest("Ongeldig wachtwoord of emailadres."); }

    var jwtToken = tokenService.CreateJWTToken(gebruiker, roles.ToList());
    var response = new LoginResponseDto {
      UserId = gebruiker.Id,
      Voornaam = gebruiker.Voornaam,
      Achternaam = gebruiker.Achternaam,
      JwtToken = jwtToken
    };

    return Ok(response);
  }


  [HttpGet]
  [Authorize]
  public async Task<IActionResult> test() {
    var userName = User?.FindFirstValue(ClaimTypes.Email);

    return Ok(userName);
  }

}
