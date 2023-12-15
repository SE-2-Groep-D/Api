using Api.Models.Domain;
using API.Models.DTO.Gebruiker;
using Api.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Api.Controllers; 

[Route("[controller]")]
[ApiController]
public class GebruikerController : ControllerBase {

    private UserManager<Gebruiker> _userManager;
    private readonly IMapper mapper;

    public GebruikerController(UserManager<Gebruiker> manager, IMapper mapper) {
        this._userManager = manager;
        this.mapper = mapper;
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
    [Route("{email}/email")]
    public async Task<IActionResult> GetByEmail([FromRoute] string email) {
        Gebruiker? user = await _userManager.FindByEmailAsync(email);

        if (user == null) {
            return NotFound("Gebruiker niet gevonden.");
        }
        
        return Ok(user);
    }
    
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetById([FromRoute] string id) {
        Gebruiker? user = await _userManager.FindByIdAsync(id);

        if (user == null) {
            return NotFound("Gebruiker niet gevonden.");
        }
        
        return Ok(user);
    }


    [HttpPut]
    [Route("{id}/update")]
    public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateGebruikerRequestDto request) {
        Gebruiker? user = await _userManager.FindByIdAsync(id);

        if (user == null) {
            return NotFound("Gebruiker niet gevonden om te updaten.");
        }

        //Voorbeeld van mapper
        //user = mapper.Map(request, user);

        user.Email = request.Email ?? user.Email;
        user.UserName = request.Email ?? user.UserName;
        user.Voornaam = request.Voornaam ?? user.Voornaam;
        user.Achternaam = request.Achternaam ?? user.Achternaam;

        var result = await _userManager.UpdateAsync(user);
        if(!result.Succeeded) return BadRequest("Kon gebruiker niet updaten.");

        if (request.Roles != null) {
            var identityResult = await _userManager.AddToRolesAsync(user, request.Roles);
            if (!identityResult.Succeeded) {
                return BadRequest("Kon rollen niet toevoegen.");
            }
        }
        
        return Ok("Gebruiker succesvol geupdate.");
    }

    [HttpDelete]
    [Route("{id}/delete")]
    public async Task<IActionResult> Delete([FromRoute] string id) {
        Gebruiker? user = await _userManager.FindByIdAsync(id);

        if (user != null) {
            var result = await _userManager.DeleteAsync(user);
            return BadRequest("Kon gebruiker niet verwijderen.");
        }
       
        return Ok("Gebruiker succesvol verwijderd.");
    }
}