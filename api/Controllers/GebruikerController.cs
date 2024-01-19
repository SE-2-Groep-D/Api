using Api.Models.Domain.User;
using Api.Models.DTO.Gebruiker;
using Api.Models.DTO.Gebruiker.request;
using Api.Services.IUserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers;
[Route("[controller]")]
[ApiController]
[Authorize]
public class GebruikerController : ControllerBase {

  private readonly IUserService _userService;

  private readonly UserManager<Gebruiker> _userManager;

  public GebruikerController(UserManager<Gebruiker> manager, IUserService service) {
    _userManager = manager;
    _userService = service;
  }

  [HttpGet]
  [Route("list")]
  //[Authorize(Roles = "Beheerder")]
  public async Task<IActionResult> GetAll() {
    var lessDetailUsers = await _userService.GetUsersAsync();
    return Ok(lessDetailUsers);
  }

  [HttpGet]
  [Route("{id}")]
  public async Task<IActionResult> GetUser([FromRoute] string id) {
    var email = User?.FindFirstValue(ClaimTypes.Email);
    var role = User?.FindFirstValue(ClaimTypes.Role);
    if (email == null || role == null) {
      return BadRequest("Ongeldig token.");
    }
    
    var user = await _userService.GetUserByIdentification(id);
    var claimedUser = await _userService.GetUserByIdentification(email);
    if (role != "Beheerder" &&  (claimedUser == null || claimedUser != user)) {
      return BadRequest("Ongeldig token.");
    }

    if (user == null) return NotFound("Gebruiker niet gevonden.");
    return Ok(_userService.GetUserDetails(user));
  }


  [HttpPut]
  [Route("{id}/update")]
  public async Task<IActionResult> Update([FromRoute] string id, [FromBody] InsertGebruikersInfoDto request) {
    var user = await _userService.GetUserByIdentification(id);
    if (user == null) return NotFound("Gebruiker niet gevonden.");

    var functionDictionary = new Dictionary<string, Action>();
    functionDictionary.Add("Roles",
      () => {
        if (request.Roles == null) return;
        _userManager.AddToRolesAsync(user, request.Roles);
      });


    var response = await _userService.UpdateUserProperties(user, request, functionDictionary);
    if (!response.Succeeded) return BadRequest(response);
    return Ok(response);
  }

  [HttpPut]
  [Route("{id}/change-password")]
  public async Task<IActionResult> ResetPassword([FromRoute] string id, [FromBody] ChangePasswordDto request) {
    var user = await _userService.GetUserByIdentification(id);
    if (user == null) return NotFound("Gebruiker niet gevonden.");
    var result = _userManager.ChangePasswordAsync(user, request.password, request.NewPassword);
    if (!result.IsCompletedSuccessfully) return BadRequest("Could not change password.");
    return Ok("Successfully changed the password.");
  }


  [HttpDelete]
  [Route("{id}/delete")]
  [Authorize(Roles = "Beheerder")]
  public async Task<IActionResult> Delete([FromRoute] string id) {
    var user = await _userManager.FindByIdAsync(id);

    if (user == null) {
      return NotFound("Gebruiker niet gevonden.");
    }

    await _userManager.DeleteAsync(user);
    return Ok("Gebruiker succesvol verwijderd.");
  }

}
