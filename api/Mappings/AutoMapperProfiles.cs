using Api.Models.Domain;
using Api.Models.DTO;
using Api.Models.DTO.Auth;
using API.Models.DTO.Gebruiker;
using API.Models.DTO.Gebruiker.response;
using API.Models.DTO.Gebruiker.response.GebruikerDetailsResponseDto;
using AutoMapper;

namespace Api.Mappings;

public class AutoMapperProfiles: Profile {
    public AutoMapperProfiles() {
        CreateMap<UpdateGebruikerRequestDto, Gebruiker>()
                .ForMember(dest => dest.Email, opt => opt.Condition(src => src.Email != null))                
                .ForMember(dest => dest.Voornaam, opt => opt.Condition(src => src.Voornaam != null))
                .ForMember(dest => dest.Achternaam, opt => opt.Condition(src => src.Achternaam != null))
                .AfterMap((src, dest) => { if (src.Email != null) dest.UserName = src.Email; });

        CreateMap<RegisterErvaringsdeskundigeRequestDto, Ervaringsdeskundige>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));

        CreateMap<Gebruiker, GebruikerDetailsResponseDto>();
        CreateMap<Medewerker, MedewerkerDto>();
        CreateMap<Ervaringsdeskundige, ErvaringsdeskundigeDto>();
        CreateMap<Bedrijf, BedrijfDto>();
        
    }
}

