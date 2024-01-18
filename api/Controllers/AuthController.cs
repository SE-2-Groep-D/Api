using System.Security.Claims;
using Api.Models.Domain.User;
using Api.Models.DTO.Auth.request;
using Api.Models.DTO.Auth.response;
using Api.Repositories.IGebruikerRepository;
using Api.Services.ITokenService;
using Api.Services.IUserService;
using AutoMapper;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
[Route("[controller]")]
[ApiController]
public class AuthController : ControllerBase {

  private readonly IConfiguration configuration;

  private readonly UserManager<Gebruiker> gebruikerManager;
  private readonly IGebruikerRepository gebruikerRepository;
  private readonly IMapper mapper;
  private readonly ITokenService tokenService;
  private readonly IUserService userService;

  public AuthController(UserManager<Gebruiker> gebruikerManager, IGebruikerRepository gebruikerRepository, IUserService userService,
    ITokenService tokenService, IMapper mapper, IConfiguration configuration) {
    this.gebruikerManager = gebruikerManager;
    this.userService = userService;
    this.tokenService = tokenService;
    this.mapper = mapper;
    this.configuration = configuration;
    this.gebruikerRepository = gebruikerRepository;
  }

  [HttpPost]
  [Route("Register")]
  [Authorize(Roles = "Beheerder")]
  public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto) {
    var gebruiker = mapper.Map<Gebruiker>(registerRequestDto);

    var result = await userService.Register(gebruiker, registerRequestDto.Password, registerRequestDto.Roles);
    return result.Succeeded ? Ok(result.Message) : BadRequest(result.Message);


  }


  [HttpPost]
  [Route("RegisterErvaringsdeskundige")]
  public async Task<IActionResult> RegisterErvaringsdeskundige([FromBody] RegisterErvaringsdeskundigeRequestDto registerErvaringsdeskundigeRequestDto) {
    var gebruiker = mapper.Map<Ervaringsdeskundige>(registerErvaringsdeskundigeRequestDto);

    string[] Roles = { "Ervaringsdeskundige" };

    var result = await userService.Register(gebruiker, registerErvaringsdeskundigeRequestDto.Password, Roles);

    var AangemaakteGebruiker = await gebruikerManager.FindByEmailAsync(gebruiker.Email);

    if (result.Succeeded && registerErvaringsdeskundigeRequestDto.NieuweHulpmiddelen != null && AangemaakteGebruiker != null) {
      var resultHulpmiddel =
        await gebruikerRepository.VoegHulpmiddelenToe(registerErvaringsdeskundigeRequestDto.NieuweHulpmiddelen, AangemaakteGebruiker.Id);
      if (!resultHulpmiddel.Succeeded) {
        return Ok(resultHulpmiddel.Message);
      }
    }

    if (result.Succeeded && registerErvaringsdeskundigeRequestDto.NieuweVoorkeursbenaderingen != null && AangemaakteGebruiker != null) {
      var resultBenadering =
        await gebruikerRepository.VoegBenaderingToe(registerErvaringsdeskundigeRequestDto.NieuweVoorkeursbenaderingen, AangemaakteGebruiker.Id);
      if (!resultBenadering.Succeeded) {
        return Ok(resultBenadering.Message);
      }
    }

    return result.Succeeded ? Ok(result.Message) : BadRequest(result.Message);
  }

  [HttpPost]
  [Route("RegisterBedrijf")]
  public async Task<IActionResult> RegisterBedrijf([FromBody] RegisterBedrijfRequestDto registerBedrijfRequestDto) {

    var gebruiker = mapper.Map<Bedrijf>(registerBedrijfRequestDto);


    string[] Roles = { "Bedrijf" };

    var result = await userService.Register(gebruiker, registerBedrijfRequestDto.Password, Roles);
    return result.Succeeded ? Ok(result.Message) : BadRequest(result.Message);
  }

  [HttpPost]
  [Route("RegisterMedwerker")]
  [Authorize(Roles = "Beheerder")]
  public async Task<IActionResult> RegisterMedewerker([FromBody] RegisterMedewerkerRequestDto registerMedewerkerRequestDto) {
    var gebruiker = mapper.Map<Medewerker>(registerMedewerkerRequestDto);

    string[] Roles = { "Medewerker" };

    var result = await userService.Register(gebruiker, registerMedewerkerRequestDto.Password, Roles);
    return result.Succeeded ? Ok(result.Message) : BadRequest(result.Message);
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

    var response = mapper.Map<LoginResponseDto>(gebruiker);

    response.UserType = GetUserType(gebruiker);

    HttpContext.Response.Cookies.Append(
        "access_token",
        jwtToken,
        new CookieOptions {
          HttpOnly = true,
          SameSite = SameSiteMode.None,
          Secure = true
        }
      );

    return Ok(response);
  }

  [HttpGet]
  [Route("Refresh")]
  [Authorize]
  public async Task<IActionResult> Refresh() {

    var userName = User?.FindFirstValue(ClaimTypes.Email);
    var gebruiker = await gebruikerManager.FindByEmailAsync(userName);
    if (gebruiker == null) { return BadRequest("Ongeldig wachtwoord of emailadres."); }

    var roles = await gebruikerManager.GetRolesAsync(gebruiker);
    if (roles == null) { return BadRequest("Ongeldig wachtwoord of emailadres."); }

    var jwtToken = tokenService.CreateJWTToken(gebruiker, roles.ToList());

    var response = mapper.Map<LoginResponseDto>(gebruiker);

    response.UserType = GetUserType(gebruiker);

    HttpContext.Response.Cookies.Append(
        "access_token",
        jwtToken,
        new CookieOptions {
          HttpOnly = true,
          SameSite = SameSiteMode.None,
          Secure = true
        }
      );

    return Ok(response);
  }

  [HttpGet]
  [Route("Logout")]
  [Authorize]
  public async Task<IActionResult> Logout() {
    var cookieOptions = new CookieOptions {
      HttpOnly = true,
      SameSite = SameSiteMode.None,
      Secure = true,
      Expires = DateTime.Now.AddDays(-1)
    };

    HttpContext.Response.Cookies.Append("access_token", "", cookieOptions);
    return Ok("Succesvol uitgelogd.");
  }


  [AllowAnonymous]
  [HttpPost("google")]
  public async Task<IActionResult> Authenticate([FromBody] GoogleRequestDto request) {
    var settings = new GoogleJsonWebSignature.ValidationSettings();

    // Change this to your google client ID

    var clientId = "169633306915-is0h5dvfs7e6cu1ic8ee17qjpf787qmn.apps.googleusercontent.com";
    if (clientId == null) { return StatusCode(500); }

    settings.Audience = new List<string> { clientId };


    try {
      var payload = GoogleJsonWebSignature.ValidateAsync(request.IdToken, settings).Result;
      var gebruiker = await userService.GetUserByIdentification(payload.Email);
      if (gebruiker == null) {
        return NotFound(new { payload.Email, Message = "U moet u nog registreren" });
      }

      var roles = await gebruikerManager.GetRolesAsync(gebruiker);
      var jwtToken = tokenService.CreateJWTToken(gebruiker, roles.ToList());

      var response = mapper.Map<LoginResponseDto>(gebruiker);
      response.UserType = GetUserType(gebruiker);

      HttpContext.Response.Cookies.Append(
          "access_token",
          jwtToken,
          new CookieOptions {
            HttpOnly = true,
            SameSite = SameSiteMode.None,
            Secure = true
          }
        );

      return Ok(response);
    } catch (Exception ex) {
      Console.WriteLine(ex);
      return BadRequest("Ongeldig token");
    }

  }


  private string GetUserType(Gebruiker gebruiker) {
    switch (gebruiker) {
      case Bedrijf:
        return "Bedrijf";

      case Ervaringsdeskundige:
        return "Ervaringsdeskundige";

      case Medewerker:
        return "Medewerker";

      default:
        return "Gebruiker";
    }
  }

}
