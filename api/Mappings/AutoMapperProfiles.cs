using Api.Models.Domain;
using Api.Models.Domain.User;
using Api.Models.DTO;
using Api.Models.DTO.Auth;
using API.Models.DTO.Gebruiker;
using Api.Models.DTO.Gebruiker.request;
using API.Models.DTO.Gebruiker.response.GebruikerDetailsResponseDto;
using AutoMapper;

namespace Api.Mappings;
public class AutoMapperProfiles : Profile {

  public AutoMapperProfiles() {
    
    CreateMap<InsertGebruikersInfoDto, Gebruiker>()
      .ForMember(dest => dest.Email, opt => opt.Condition(src => src.Email != null))
      .ForMember(dest => dest.Voornaam, opt => opt.Condition(src => src.Voornaam != null))
      .ForMember(dest => dest.Achternaam, opt => opt.Condition(src => src.Achternaam != null))
      .AfterMap((src, dest) => {
        if (src.Email != null) dest.UserName = src.Email;
      });

    CreateMap<RegisterRequestDto, Gebruiker>()
      .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
    CreateMap<RegisterErvaringsdeskundigeRequestDto, Ervaringsdeskundige>()
      .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
    CreateMap<RegisterBedrijfRequestDto, Bedrijf>()
      .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
    CreateMap<RegisterMedewerkerRequestDto, Medewerker>()
      .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
   
    
    CreateMap<Gebruiker, GebruikerDetails>().ReverseMap();
    CreateMap<Medewerker, MedewerkerDetails>().ReverseMap();
    CreateMap<Ervaringsdeskundige, ErvaringsDeskundigeDetails>().ReverseMap();
    CreateMap<Bedrijf, BedrijfsDetails>().ReverseMap();
    
  }

}
