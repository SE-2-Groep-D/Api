
using Api.Models.Domain.User;
using API.Models.DTO.Gebruiker;
using API.Models.DTO.Gebruiker.request.UpdateGebruikerRequestDto;
using Api.Services.IUserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[Route("[controller]")]
[ApiController]
public class GebruikerController : ControllerBase {

  private UserManager<Gebruiker> _userManager;
  private readonly IUserService _userService;

  public GebruikerController(UserManager<Gebruiker> manager, IUserService service) {
    _userManager = manager;
    _userService = service;
  }

  [HttpGet]
  [Route("list")]
  public async Task<IActionResult> GetAll() {
    var users = await _userManager.Users.ToListAsync();

    if (users.Count == 0) {
      return NotFound("Geen gebruikers gevonden.");
    }

    return Ok(users);
  }

  [HttpGet]
  [Route("{id}")]
  public async Task<IActionResult> GetUser([FromRoute] string id) {
    Gebruiker? user = await _userService.GetUserByIdentification(id);
    if (user == null) return NotFound("Gebruiker niet gevonden.");
    return Ok(_userService.GetUserDetails(user));
  }


  [HttpPut]
  [Route("update/{id}")]
  public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateGebruikerRequestDto request) {
    Gebruiker? user = await _userService.GetUserByIdentification(id);

    if (user == null) return NotFound("Gebruiker niet gevonden.");
    var succeeded = await _userService.UpdateUser(user, request);
    
    if (!succeeded) return BadRequest("Kon gebruiker niet updaten.");
    return Ok("Gebruiker succesvol geupdate.");
  }

  [HttpDelete]
  [Route("delete/{id}")]
  public async Task<IActionResult> Delete([FromRoute] string id) {
    Gebruiker? user = await _userManager.FindByIdAsync(id);

    if (user == null) {
      return NotFound("Gebruiker niet gevonden.");
    }

    await _userManager.DeleteAsync(user);
    return Ok("Gebruiker succesvol verwijderd.");
  }
}
