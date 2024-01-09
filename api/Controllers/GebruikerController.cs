using Api.Models.Domain.User;
using Api.Models.DTO.Gebruiker;
using Api.Models.DTO.Gebruiker.request;
using Api.Services.IUserService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
    var lessDetailUsers = await _userService.GetUsersAsync();
    return Ok(lessDetailUsers);
  }

  [HttpGet]
  [Route("{id}")]
  public async Task<IActionResult> GetUser([FromRoute] string id) {
    Gebruiker? user = await _userService.GetUserByIdentification(id);
    if (user == null) return NotFound("Gebruiker niet gevonden.");
    return Ok(_userService.GetUserDetails(user));
  }


  [HttpPut]
  [Route("{id}/update")]
  public async Task<IActionResult> Update([FromRoute] string id, [FromBody] InsertGebruikersInfoDto request) {
    Gebruiker? user = await _userService.GetUserByIdentification(id);
    if (user == null) return NotFound("Gebruiker niet gevonden.");

    Dictionary<string, Action> functionDictionary = new Dictionary<string, Action>();
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
    Gebruiker? user = await _userService.GetUserByIdentification(id);
    if (user == null) return NotFound("Gebruiker niet gevonden.");
    var result = _userManager.ChangePasswordAsync(user, request.password, request.NewPassword);
    if (!result.IsCompletedSuccessfully) return BadRequest("Could not change password.");
    return Ok("Successfully changed the password.");
  }


  [HttpDelete]
  [Route("{id}/delete")]
  public async Task<IActionResult> Delete([FromRoute] string id) {
    Gebruiker? user = await _userManager.FindByIdAsync(id);

    if (user == null) {
      return NotFound("Gebruiker niet gevonden.");
    }

    await _userManager.DeleteAsync(user);
    return Ok("Gebruiker succesvol verwijderd.");
  }

}
