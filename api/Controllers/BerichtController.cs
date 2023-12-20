using Api.Models.Domain.Bericht;
using Api.Models.DTO.Bericht;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Api.Repositories.IBerichtRepository;
using System.Runtime.CompilerServices;
namespace Api.Controllers {


  //localhost:3001/bericht
  [Route("[controller]")]
  [ApiController]
  public class BerichtController : ControllerBase {

    private IBerichtRepository BerichtRepository;

    public BerichtController(IBerichtRepository berichtRepository) {
      
      this.BerichtRepository = berichtRepository;

    }

    //get
    //localhost:3001/bericht/chats/{id}

    [HttpGet]
    [Route("chats/{id}")]
    public async Task<IActionResult> GetChats([FromRoute] string id) {

      return Ok();

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

  }
}
