using Api.Models.Domain;
using Api.Models.Domain.Research;
using Api.Models.Domain.Research.Questionlist;
using Api.Models.Domain.Research.Tracking;
using Api.Models.Domain.User;
using Api.Models.DTO.Auth.request;
using Api.Models.DTO.Auth.response;
using API.Models.DTO.Gebruiker;
using Api.Models.DTO.Gebruiker.request;
using API.Models.DTO.Gebruiker.response.GebruikerDetailsResponseDto;
using Api.Models.DTO.Nieuwsbrief;
using Api.Models.DTO.Onderzoek;
using Api.Models.DTO.Onderzoek.request;
using Api.Models.DTO.Onderzoek.response;
using Api.Models.DTO.Onderzoek.results;
using Api.Models.DTO.Onderzoek.tracking;
using AutoMapper;
using Api.Models.Domain.Bericht;
using Api.Models.DTO.Bericht;
using Api.Models.DTO.Gebruiker;


namespace Api.Mappings;
public class AutoMapperProfiles : Profile
{

  public AutoMapperProfiles()
  {
    CreateMap<InsertGebruikersInfoDto, Gebruiker>()
      .ForMember(dest => dest.Email, opt => opt.Condition(src => src.Email != null))
      .ForMember(dest => dest.Voornaam, opt => opt.Condition(src => src.Voornaam != null))
      .ForMember(dest => dest.Achternaam, opt => opt.Condition(src => src.Achternaam != null))
      .AfterMap((src, dest) =>
      {
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
    CreateMap<Hulpmiddel, HulpmiddelDto>();
    CreateMap<Voorkeurbenadering, VoorkeurbenaderingDto>();
    CreateMap<Bedrijf, BedrijfsDetails>();

    CreateMap<CreateNiewsbriefDto, Nieuwsbrief>();
    CreateMap<UpdateNieuwsbriefDto, Nieuwsbrief>();
    CreateMap<Nieuwsbrief, NieuwsBriefDto>().ForMember(dest => dest.Medewerker, opt => opt.MapFrom(src => src.Medewerker));
    CreateMap<Medewerker, NieuwsbriefMedewerkerDto>();


    CreateMap<UpdateOnderzoekRequestDto, Onderzoek>()
      .ForMember(dest => dest.StartDatum, opt => opt.Condition(src => src.StartDatum != null))
      .ForMember(dest => dest.Titel, opt => opt.Condition(src => src.Titel != null))
      .ForMember(dest => dest.AantalParticipanten, opt => opt.Condition(src => src.AantalParticipanten != null))
      .ForMember(dest => dest.websiteUrl, opt => opt.Condition(src => src.websiteUrl != null))
      .ForMember(dest => dest.Omschrijving, opt => opt.Condition(src => src.Omschrijving != null))
      .ForMember(dest => dest.Vergoeding, opt => opt.Condition(src => src.Locatie != null))
      .ForMember(dest => dest.Locatie, opt => opt.Condition(src => src.Locatie != null))
      .ForMember(dest => dest.Status, opt => opt.Condition(src => src.Status != null));

    CreateMap<OnderzoekDto, Onderzoek>().ReverseMap();
    CreateMap<AddOnderzoekRequestDto, Onderzoek>();




    // QuestionList
    CreateMap<CreateQuestionListDto, QuestionList>()
      .ForMember(dest => dest.Title, opt => opt.Condition(src => src.Title != null))
      .ForMember(dest => dest.Description, opt => opt.Condition(src => src.Description != null));

    CreateMap<CreateQuestionDto, Question>()
      .ForMember(dest => dest.Description, opt => opt.Condition(src => src.Description != null))
      .ForMember(dest => dest.Type, opt => opt.Condition(src => src.Type != null));
    // .ForMember(dest => dest.PossibleAnswers, opt => opt.MapFrom(src => src.PossibleAnswers));

    CreateMap<CreatePossibleAnswerDto, PossibleAnswer>();

    CreateMap<QuestionListDto, QuestionList>().ReverseMap();
    CreateMap<BigQuestionListDto, QuestionList>().ReverseMap();
    CreateMap<QuestionList, MinimalQuestionListDto>().ReverseMap();

    CreateMap<Answer, AnswerDto>();
    CreateMap<PossibleAnswer, AnswerDto>();
    CreateMap<Question, QuestionDto>()
      .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))
      .ForMember(dest => dest.PossibleAnswers, opt => opt.MapFrom(src => src.PossibleAnswers));
    CreateMap<QuestionList, QuestionListDto>()
      .ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.Questions));

    CreateMap<UpdateQuestionListDto, QuestionList>()
      .ForMember(dest => dest.Title, opt => opt.Condition(src => src.Title != null))
      .ForMember(dest => dest.Description, opt => opt.Condition(src => src.Description != null));

    CreateMap<UpdateQuestionDto, Question>()
      .ForMember(dest => dest.Description, opt => opt.Condition(src => src.Description != null))
      .ForMember(dest => dest.Type, opt => opt.Condition(src => src.Type != null));
    // .ForMember(dest => dest.PossibleAnswers, opt => opt.MapFrom(src => src.PossibleAnswers));

    CreateMap<UpdatePossibleAnswerDto, PossibleAnswer>();

    CreateMap<SubmitAnswerDto, Answer>();
    CreateMap<Answer, ResponseAnswerDto>();

    // CreateMap<Answer, ResponseAnswerDto>();


    // Tracking onderzoek
    CreateMap<QuestionList, ResponseQuestionListDto>().ReverseMap();
    CreateMap<TrackingOnderzoek, ResponseTrackingDto>();
    CreateMap<CreateTrackingResearchDto, TrackingOnderzoek>().ReverseMap();
    CreateMap<OnderzoekErvaringsdekundige, AddRegistrationDto>().ReverseMap();

    CreateMap<Bericht, BerichtDto>().ReverseMap();
  }

}
