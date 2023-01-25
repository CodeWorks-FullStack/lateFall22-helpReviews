namespace helpReviews.Models;


public class Profile
{
  public string Id { get; set; }
  public string Name { get; set; }
  public string Picture { get; set; }
  public string BrandImg { get; set; }
}

public class Account : Profile
{
  public string Email { get; set; }
}
