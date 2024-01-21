namespace Api.Models.DTO.Bericht {
  public class BerichtDto {
    public Guid Id { get; set; }
    public DateTime DatumTijd { get; set; }
    public string Tekst { get; set; }
    public Guid VerzenderId { get; set; }
    public Guid OntvangerId { get; set; }
    
  }
}
