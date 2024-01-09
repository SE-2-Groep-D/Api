
using Api.Models.Domain.Research;
using Api.Models.Domain.Research.Tracking;
using Api.Models.Domain.User;
using API.Models.DTO.Gebruiker;
using Api.Models.DTO.Gebruiker.request;
using API.Models.DTO.Gebruiker.response.GebruikerDetailsResponseDto;
using Api.Models.DTO.Nieuwsbrief;
using Api.Models.DTO.Onderzoek;
using Api.Models.DTO.Onderzoek.results;
using Api.Models.DTO.Onderzoek.tracking;
using AutoMapper;
using Api.Models.DTO.Auth.request;
using Api.Models.DTO.Auth.response;

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
    CreateMap<RegisterErvaringsdeskundigeRequestDto, Ervaringsdeskundige>()
      .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));

    CreateMap<Gebruiker, LoginResponseDto>();

    CreateMap<Gebruiker, GebruikerDetails>();
    CreateMap<Medewerker, MedewerkerDetails>();
    CreateMap<Ervaringsdeskundige, ErvaringsDeskundigeDetails>();
    CreateMap<Bedrijf, BedrijfsDetails>();

    CreateMap<CreateNiewsbriefDto, Nieuwsbrief>();
    CreateMap<UpdateNieuwsbriefDto, Nieuwsbrief>();

    CreateMap<UpdateOnderzoekRequestDto, Onderzoek>()
      .ForMember(dest => dest.StartDatum, opt => opt.Condition(src => src.StartDatum != null))
      .ForMember(dest => dest.Titel, opt => opt.Condition(src => src.Titel != null))
      .ForMember(dest => dest.AantalParticipanten, opt => opt.Condition(src => src.AantalParticipanten != null))
      .ForMember(dest => dest.websiteUrl, opt => opt.Condition(src => src.websiteUrl != null))
      .ForMember(dest => dest.Omschrijving, opt => opt.Condition(src => src.Omschrijving != null))
      .ForMember(dest => dest.Vergoeding, opt => opt.Condition(src => src.Locatie != null))
      .ForMember(dest => dest.Locatie, opt => opt.Condition(src => src.Locatie != null))
      .ForMember(dest => dest.Status, opt => opt.Condition(src => src.Status != null));


    CreateMap<UpdateVragenlijstRequestDto, Vragenlijst>()
      .ForMember(dest => dest.Titel, opt => opt.Condition(src => src.Titel != null))
      .ForMember(dest => dest.Samenvatting, opt => opt.Condition(src => src.Samenvatting != null));


    CreateMap<OnderzoekDto, Onderzoek>().ReverseMap();
    CreateMap<AddOnderzoekRequestDto, Onderzoek>();


    //voor vragenlijst
    
    CreateMap<Vragenlijst, VragenlijstDto>()
      .ForMember(dest => dest.Vragen, opt => opt.MapFrom(src => src.Vragen));
        
    CreateMap<Vraag, VraagDTO>()
      .ForMember(dest => dest.Antwoorden, opt => opt.MapFrom(src => src.Antwoorden));
        
    CreateMap<Antwoord, AntwoordDTO>();
    CreateMap<AddVragenlijstRequestDto, Vragenlijst>().ReverseMap();


    //Voor vraag

    CreateMap<UpdateVraagRequestDto, Vraag>()
      .ForMember(dest => dest.Type, opt => opt.Condition(src => src.Type != null))
      .ForMember(dest => dest.Onderwerp, opt => opt.Condition(src => src.Onderwerp != null));
    
    CreateMap<AddVraagRequestDto, Vraag>().ReverseMap();

    //Voor antwoord

  
    CreateMap<UpdateAntwoordRequestDto, Antwoord>()
      .ForMember(dest => dest.Tekst, opt => opt.Condition(src => src.Tekst != null));
    
    CreateMap<AddAntwoordRequestDto, Antwoord>()
      .ForMember(dest => dest.VraagId, opt => opt.MapFrom(src => src.VraagId)) 
      .ReverseMap();
    CreateMap<CreateTrackingResearchDto, TrackingOnderzoek>();
    CreateMap<UpdateTrackingResearchDto, TrackingOnderzoek>();
    CreateMap<SubmitTrackingResultsDto, TrackingResultaten>();
    CreateMap<ClickedItemDto, ClickedItem>();

    CreateMap<TrackingOnderzoek, ResponseTrackingDto>().ReverseMap();
    
 
    // Voor results
    
    CreateMap<TrackingOnderzoek, ResponseTrackingDto>();
    CreateMap<Vragenlijst, ResponseVragenlijstDto>();
  }

}