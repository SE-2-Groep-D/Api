using Api.Data;
using Api.Models.Domain.Bericht;
using Api.Models.DTO.Bericht;
using Api.Models.DTO.Bericht;
using Api.Services.IUserService;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;

namespace Api.Repositories.IBerichtRepository {
  public class SQLBerichtRepository : IBerichtRepository {

    private AccessibilityDbContext _context;
    private readonly IUserService userService;
    private readonly IMapper mapper;

    public SQLBerichtRepository(AccessibilityDbContext context, IUserService userService, IMapper mapper) {
      this._context = context;
      this.userService = userService;
      this.mapper = mapper;
    }

    public async Task<Bericht> CreateBericht(Bericht bericht) {

      await _context.Berichten.AddAsync(bericht);
      await _context.SaveChangesAsync();
      return bericht;

    }

    public async Task<IEnumerable<ChatResponseDto>> GetChatsByUserId(Guid userId) {
      var berichten = await _context.Berichten
          .Where(b => b.VerzenderId == userId || b.OntvangerId == userId)
          .ToListAsync();


      var groupedChats = berichten
          .GroupBy(chat => chat.VerzenderId == userId ? chat.OntvangerId : chat.VerzenderId)
          .Select( group => new ChatResponseDto {
            OtherUserId = group.Key,
            LastMessage = mapper.Map<BerichtDto>(group.OrderByDescending(m => m.DatumTijd).First()),
            TotalMessages = group.Count()
          });

      return  groupedChats;
    }

    public async Task<string> GetNaam(string userId) {


      var gebruiker = await userService.GetUserByIdentification(userId);
      var naam = gebruiker.Voornaam;


      return naam;
    }




    public async Task<IEnumerable<Bericht>> GetBerichten(Guid verzenderId, Guid ontvangerId) {
      return await _context.Berichten
          .Where(b => (b.VerzenderId == verzenderId && b.OntvangerId == ontvangerId) ||
                      (b.VerzenderId == ontvangerId && b.OntvangerId == verzenderId))
          .OrderBy(b => b.DatumTijd)
          .ToListAsync();
    }

  }
}
