namespace Api.Models.DTO.Nieuwsbrief; 
public class NieuwsBriefDto {

  public Guid Id { get; set; }
  public DateTime Datum { get; set; }
  public string Titel { get; set; }
  public string Inhoud { get; set; }

  public NieuwsbriefMedewerkerDto Medewerker { get; set; }
}

public class NieuwsbriefMedewerkerDto {
  public Guid Id { get; set; } 
  public string Voornaam { get; set; }
  public string Achternaam { get; set; }
}