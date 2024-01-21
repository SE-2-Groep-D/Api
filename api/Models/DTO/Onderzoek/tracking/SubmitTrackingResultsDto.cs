namespace Api.Models.DTO.Onderzoek.tracking;
public class SubmitTrackingResultsDto {

  public string Domain { get; set; }
  public string Page { get; set; }
  public string PagePercentage { get; set; }
  public string Browser { get; set; }
  public int TimeInSeconds { get; set; }
  public int TimeTillAction { get; set; }
  public List<ClickedItemDto> ClickedItems { get; set; }

}

public class ClickedItemDto {

  public string? ItemId { get; set; }
  public string itemType { get; set; }
  public int TimeInSeconds { get; set; }
  public string Text { get; set; }
  public string? Href { get; set; }

}
