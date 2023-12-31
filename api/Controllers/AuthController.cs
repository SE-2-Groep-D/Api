﻿using Api.Models.Domain;
using Api.Services.ITokenService;
using Api.Services.IUserService;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Api.Models.Domain.User;
using static Azure.Core.HttpHeader;
using System.Xml;
using System.ComponentModel.DataAnnotations;
using Google.Apis.Auth;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Net;
using Api.Models.DTO.Auth.request;
using Api.Repositories.IGebruikerRepository;

namespace Api.Controllers;
[Route("[controller]")]
[ApiController]
public class AuthController : ControllerBase {

  private readonly UserManager<Gebruiker> gebruikerManager;
  private readonly IUserService userService;
  private readonly ITokenService tokenService;
  private readonly IMapper mapper;
  private readonly IConfiguration configuration;
  private readonly IGebruikerRepository gebruikerRepository;

  public AuthController(UserManager<Gebruiker> gebruikerManager, IGebruikerRepository gebruikerRepository, IUserService userService, ITokenService tokenService, IMapper mapper, IConfiguration configuration) {
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
      var resultHulpmiddel = await gebruikerRepository.VoegHulpmiddelenToe(registerErvaringsdeskundigeRequestDto.NieuweHulpmiddelen, AangemaakteGebruiker.Id);
      if (!resultHulpmiddel.Succeeded) {
        return Ok(resultHulpmiddel.Message);
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
    var response = userService.CreateLoginResponse(gebruiker, jwtToken);

    return Ok(response);
  }


  [AllowAnonymous]
  [HttpPost("google")]
  public async Task<IActionResult> Authenticate([FromBody] GoogleRequestDto request) {
    GoogleJsonWebSignature.ValidationSettings settings = new GoogleJsonWebSignature.ValidationSettings();

    // Change this to your google client ID
    
    string clientId = configuration["Authentication:Google:ClientId"];
    if(clientId == null) { return StatusCode(500); }
    settings.Audience = new List<string>() { clientId };


    try {
      GoogleJsonWebSignature.Payload payload = GoogleJsonWebSignature.ValidateAsync(request.IdToken, settings).Result;
      var gebruiker = await userService.GetUserByIdentification(payload.Email);
      if(gebruiker == null) {
        return NotFound(new {Email = payload.Email, Message = "U moet u nog registreren"});
      }

      var roles = await gebruikerManager.GetRolesAsync(gebruiker);
      var jwtToken = tokenService.CreateJWTToken(gebruiker, roles.ToList());

      var response = userService.CreateLoginResponse(gebruiker, jwtToken);

      return Ok(response);
    } catch (Exception ex) {
      Console.WriteLine(ex);
      return BadRequest("Ongeldig token");
    }
    
  }



  [HttpGet]
  [Authorize]
  public async Task<IActionResult> test() {
    //var userName = User?.FindFirstValue(ClaimTypes.Email);

    return Ok();
  }

}
