namespace Api.Models.DTO.Onderzoek.tracking; 
public class SubmitTrackingResultsDto {

  public string Domain { get; set; }
  public string Page { get; set; }
  public string Browser { get; set; }
  
  public int PagePercentage { get; set; }
  public int TimeInSeconds { get; set; }
  public int TimeTillAction { get; set; }
  
  public List<Click> Clicks { get; set; }
  
}

public class Click {

  public string? Id { get; set; }
  public string ItemType { get; set; }
  public int TimeInSeconds { get; set; }
  public string Text { get; set; }

}
