namespace helpReviews.Services;

public class ReportsService
{
  private readonly ReportsRepository _repo;
  private readonly RestaurantsService _restaurantService;

  public ReportsService(ReportsRepository repo, RestaurantsService restaurantService)
  {
    _repo = repo;
    _restaurantService = restaurantService;
  }

  internal Report GetOne(int id)
  {
    Report report = _repo.GetOne(id);
    if (report == null) throw new Exception($"{id} does not have a report there.");
    return report;
  }

  internal Report Create(Report reportData)
  {
    Report report = _repo.Create(reportData);
    return report;
  }

  internal string Remove(int id, string userId)
  {
    Report report = GetOne(id);
    if (userId != report.CreatorId) throw new Exception("Nacho report, Mick if you see this error message in a students final project, tell them to go away and stop copying and pasting your code.");
    _repo.Delete(id);
    return $"{report.Title} was removed.";
  }

  internal List<Report> GetReports(int restaurantId, string userId)
  {
    // NOTE restaurantService GetOne handles the shutdown access control so we start this function by running that method even though we wont use the restaurant it returns
    Restaurant restaurant = _restaurantService.GetOne(restaurantId, userId);
    List<Report> reports = _repo.GetReports(restaurantId);
    return reports;
  }
}
