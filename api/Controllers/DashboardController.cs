using Api.Models.Domain.User;
using Api.Services.IUserService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Api.Data;
using Api.Models.Domain.Research;
using Api.Models.DTO.Gebruiker;
using Api.Models.DTO.Onderzoek;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;
[Route("[controller]")]
[ApiController]
public class DashboardController : ControllerBase {

  private readonly IMapper _mapper;
  private readonly IUserService _userService;
  private readonly AccessibilityDbContext _dbContext;

  public DashboardController(IUserService service, AccessibilityDbContext dbContext, IMapper mapper) {
    _userService = service;
    _dbContext = dbContext;
    _mapper = mapper;
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> GetData([FromRoute] string id) {
    var userData = await GetDashboardDataAsync(id);
    if (userData == null) return NotFound();

    return Ok(userData);
  }


  private async Task<object?> GetDashboardDataAsync(string id) {
    Gebruiker? gebruiker = await _userService.GetUserByIdentification(id);
    if (gebruiker == null) return null;

    var userType = GetUserType(gebruiker);
    var userNews = await _dbContext.Nieuws.ToListAsync();
    var userAgenda = await GetUserAgenda(gebruiker);
    var userStatistics = await GetUserStatistics(gebruiker);

    var userData = new {
      type = userType,
      news = userNews,
      agenda = userAgenda,
      statistics = userStatistics
    };

    return userData;
  }

  private async Task<List<OnderzoekAgendaDto>> GetUserAgenda(Gebruiker gebruiker) {
    List<OnderzoekAgendaDto> agenda = new List<OnderzoekAgendaDto>();

    switch (gebruiker) {
      case Bedrijf bedrijf:
        agenda = await _dbContext.Onderzoeken
          .Where(on => on.BedrijfId == bedrijf.Id)
          .Select(onderzoek => new OnderzoekAgendaDto {
            Id = onderzoek.Id,
            Company = onderzoek.Bedrijf.Bedrijfsnaam,
            Status = onderzoek.Status.ToString(),
            Date = onderzoek.StartDatum,
            Participants = onderzoek.AantalParticipanten,
            Title = onderzoek.Titel,
          })
          .ToListAsync();
        break;

      case Ervaringsdeskundige ervaringsdeskundige:
        agenda = await _dbContext.OnderzoekErvaringsdekundigen
          .Where(oe => oe.ErvaringsdeskundigeId == ervaringsdeskundige.Id)
          .Select(oe => oe.Onderzoek)
          .Select(onderzoek => new OnderzoekAgendaDto {
            Id = onderzoek.Id,
            Company = onderzoek.Bedrijf.Bedrijfsnaam,
            Status = onderzoek.Status.ToString(),
            Date = onderzoek.StartDatum,
            Participants = onderzoek.AantalParticipanten,
            Title = onderzoek.Titel,
          })
          .ToListAsync();
        break;

      default:
        agenda = await _dbContext.Onderzoeken
          .Select(onderzoek => new OnderzoekAgendaDto {
            Id = onderzoek.Id,
            Company = onderzoek.Bedrijf.Bedrijfsnaam,
            Status = onderzoek.Status.ToString(),
            Date = onderzoek.StartDatum,
            Participants = onderzoek.AantalParticipanten,
            Title = onderzoek.Titel,
          })
          .ToListAsync();
        break;
    }

    return agenda;
  }

  private async Task<List<object>> GetUserStatistics(Gebruiker gebruiker) {
    List<object> returned = new List<object>();
    
    switch (gebruiker) {
      case Bedrijf bedrijf:
        var onderzoeken = await _dbContext.Onderzoeken.Where(onderzoek => onderzoek.BedrijfId == gebruiker.Id).ToListAsync();
        var active = onderzoeken.Count(o => o.Status == Status.active);
        var open = onderzoeken.Count(o => o.Status == Status.open);
        var voltooid = onderzoeken.Count(o => o.Status == Status.ended);
        
        returned.Add(new StatisticDto {
          Title = "Actieve onderzoeken",
          Value = active
        });
        
        returned.Add(new StatisticDto {
          Title = "Openstaande onderzoeken",
          Value = open
        });
        
        returned.Add(new StatisticDto {
          Title = "Voltooide onderzoeken",
          Value = voltooid
        });
        break;

      case Ervaringsdeskundige ervaringsdeskundige:
        onderzoeken = await _dbContext.OnderzoekErvaringsdekundigen.Where(onderzoek => onderzoek.ErvaringsdeskundigeId == ervaringsdeskundige.Id).Select(on => on.Onderzoek).ToListAsync();
        open = onderzoeken.Count(o => o.Status == Status.open);
        voltooid = onderzoeken.Count(o => o.Status == Status.ended);
        
        returned.Add(new StatisticDto {
          Title = "Ingeschreven opdrachten",
          Value = open
        });
        
        returned.Add(new StatisticDto {
          Title = "Voltooide opdrachten",
          Value = voltooid
        });
        
        returned.Add(new StatisticDto {
          Title = "Voltooide onderzoeken",
          Value = voltooid
        });
        break;
      
      default:
        onderzoeken = await _dbContext.Onderzoeken.ToListAsync();
        active = onderzoeken.Count(o => o.Status == Status.active);
        open = onderzoeken.Count(o => o.Status == Status.open);
        voltooid = onderzoeken.Count(o => o.Status == Status.ended);
        
        returned.Add(new StatisticDto {
          Title = "Actieve onderzoeken",
          Value = active
        });
        
        returned.Add(new StatisticDto {
          Title = "Openstaande onderzoeken",
          Value = open
        });
        
        returned.Add(new StatisticDto {
          Title = "Voltooide onderzoeken",
          Value = voltooid
        });
        
        break;
    }
    
    return returned;
  }


  private string GetUserType(Gebruiker gebruiker) {
    switch (gebruiker) {
      case Bedrijf:
        return "Bedrijf";

      case Ervaringsdeskundige:
        return "Ervaringsdeskundige";

      default:
        return "Gebruiker";
    }
  }

}
