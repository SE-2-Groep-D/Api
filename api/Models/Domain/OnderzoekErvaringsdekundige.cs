namespace Api.Models.Domain;
public class OnderzoekErvaringsdekundige {
  
  public Guid OnderzoekId { get; set; }
  public Guid ErvaringsdeskundigeId { get; set; }
  public DateTime datum { get; set; }

}