using Api.Models.Domain.Bericht;
using Api.Models.DTO.Bericht;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Api.Repositories.IBerichtRepository;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers {


  //localhost:3001/bericht
  [Route("[controller]")]
  [ApiController]
  [Authorize]
  public class BerichtController : ControllerBase {

    private IBerichtRepository BerichtRepository;

    public BerichtController(IBerichtRepository berichtRepository) {
      
      this.BerichtRepository = berichtRepository;

    }

    //get
    //localhost:3001/bericht/chats/{id}

    [HttpGet]
    [Route("chats/{id}")]
    public async Task<IActionResult> GetChats([FromRoute] Guid id) {
      var chats = await BerichtRepository.GetChatsByUserId(id);
      var groupedChats = chats
          .GroupBy(chat => chat.VerzenderId == id ? chat.OntvangerId : chat.VerzenderId)
          .Select(group => new {
            OtherUserId = group.Key,
            LastMessage = group.OrderByDescending(m => m.DatumTijd).First(),
            TotalMessages = group.Count()
          });

      return Ok(groupedChats);
    }

    [HttpPost]
    [Route("stuurbericht")]
    public async Task<IActionResult> StuurBericht([FromBody] StuurBerichtRequestDto request) {


      var bericht = new Bericht {
        Tekst = request.Tekst,
        DatumTijd = DateTime.Now,
        VerzenderId = request.VerzenderId,
        OntvangerId = request.OntvangerId,
      };

      bericht = await BerichtRepository.CreateBericht(bericht);

      return Ok(bericht);

    }

    [HttpGet]
    [Route("getberichten/{verzenderId}/{ontvangerId}")]
    public async Task<IActionResult> GetBerichten(Guid verzenderId, Guid ontvangerId) {
      var messages = await BerichtRepository.GetBerichten(verzenderId, ontvangerId);
      return Ok(messages);
    }

  }
}
