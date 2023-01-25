namespace helpReviews.Models;

public class Restaurant
{
  public int Id { get; set; }
  public string Name { get; set; }
  public string Description { get; set; }
  public string coverImg { get; set; }
  public string OwnerId { get; set; }
  public int ReportCount { get; set; }
  public int Exposure { get; set; }
  public bool? Shutdown { get; set; }
}
