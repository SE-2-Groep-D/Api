﻿using Api.Models.Domain;
using Api.Models.Domain.User;
using Api.Models.DTO;
using Api.Models.DTO.Auth;
using API.Models.DTO.Gebruiker;
using API.Models.DTO.Gebruiker.response.GebruikerDetailsResponseDto;
using AutoMapper;

namespace Api.Mappings;
public class AutoMapperProfiles : Profile {

  public AutoMapperProfiles() {
    CreateMap<UpdateGebruikerRequestDto, Gebruiker>()
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

    CreateMap<Gebruiker, GebruikerDetailsResponseDto>();
    CreateMap<Medewerker, MedewerkerDto>();
    CreateMap<Ervaringsdeskundige, ErvaringsdeskundigeDto>();
    CreateMap<Bedrijf, BedrijfDto>();

    CreateMap<CreateNiewsbriefDto, Nieuwsbrief>();
    CreateMap<UpdateNieuwsbriefDto, Nieuwsbrief>();
    
    CreateMap<UpdateOnderzoekRequestDto, Onderzoek>()
      .ForMember(dest => dest.StartDatum, opt => opt.Condition(src => src.StartDatum != null))
      .ForMember(dest => dest.Omschrijving, opt => opt.Condition(src => src.Omschrijving != null))
      .ForMember(dest => dest.Vergoeding, opt => opt.Condition(src => src.Locatie != null))
      .ForMember(dest => dest.Locatie, opt => opt.Condition(src => src.Locatie != null))
      .ForMember(dest => dest.Status, opt => opt.Condition(src => src.Status != null));


    CreateMap<UpdateVragenlijstRequestDto, Vragenlijst>()
      .ForMember(dest => dest.Titel, opt => opt.Condition(src => src.Titel != null))
      .ForMember(dest => dest.Samenvatting, opt => opt.Condition(src => src.Samenvatting != null));

    
    //voor vragenlijst
    CreateMap<VragenlijstDto, Vragenlijst>().ReverseMap();
    CreateMap<AddVragenlijstRequestDto, Vragenlijst>().ReverseMap();
  }

}
