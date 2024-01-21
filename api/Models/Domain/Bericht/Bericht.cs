using Api.Models.Domain.User;

namespace Api.Models.Domain.Bericht;
  public class Bericht {

    public Guid Id { get; set; }
    public DateTime DatumTijd {get; set;} 
    public string Tekst { get; set;}
    public Guid VerzenderId { get; set;}
    public Guid OntvangerId {  get; set;}
    public Gebruiker Verzender {  get; set;} = null!;
    public Gebruiker Ontvanger {  get; set;} = null!;

  }

