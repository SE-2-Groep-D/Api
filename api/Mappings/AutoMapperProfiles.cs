using Api.Models.Domain;
using API.Models.DTO.Gebruiker;
using AutoMapper;

namespace Api.Mappings
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UpdateGebruikerRequestDto, Gebruiker>()
                .ForMember(dest => dest.Email, opt => opt.Condition(src => src.Email != null))                
                .ForMember(dest => dest.Voornaam, opt => opt.Condition(src => src.Voornaam != null))
                .ForMember(dest => dest.Achternaam, opt => opt.Condition(src => src.Achternaam != null))
                .AfterMap((src, dest) => { if (src.Email != null) dest.UserName = src.Email; });


        }
    }
}
