namespace Api.Models.Domain.Research.Tracking;
public class TrackingResultaten {

  public Guid Id { get; set; }
  public string Page { get; set; }
  public int PagePercentage { get; set; }
  public string Browser { get; set; }
  public int TimeInSeconds { get; set; }
  public int TimeTillAction { get; set; }
  public ICollection<ClickedItem> ClickedItems { get; set; } = new List<ClickedItem>();

}

public class ClickedItem {

  public Guid Id { get; set; }
  public string? ItemId { get; set; }
  public string itemType { get; set; }
  public int TimeInSeconds { get; set; }
  public string Text { get; set; }
  public string? Href { get; set; }

}
