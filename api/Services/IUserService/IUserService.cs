using Api.Models.Domain;

namespace Api.Services.IUserService
{
    public interface IUserService
    {
        public Task<string> Register(Gebruiker gebruiker, string password, string[] roles);
        //public Task<string> Register(Ervaringsdeskundige ervaringsdeskundige, string password, string[] roles);
        //public Task<string> Register(Bedrijf ervaringsdeskundige, string password, string[] roles);
        //public Task<string> Register(Ervaringsdeskundige ervaringsdeskundige, string password, string[] roles);
    }
}
