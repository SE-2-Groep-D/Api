using Api.Models.Domain;
using Api.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers; 

public class GebruikerController : ControllerBase {

    private UserManager<Gebruiker> _userManager;
    private IGebruikerRepository _gebruikerRepository;
    
    public GebruikerController(UserManager<Gebruiker> manager, IGebruikerRepository repository) {
        this._userManager = manager;
        this._gebruikerRepository = repository;
    }
    
}