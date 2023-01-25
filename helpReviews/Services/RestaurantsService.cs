namespace helpReviews.Services;

public class RestaurantsService
{
  private readonly RestaurantsRepository _repo;

  public RestaurantsService(RestaurantsRepository repo)
  {
    _repo = repo;
  }

  internal List<Restaurant> Get(string userId)
  {
    // get all restaurants
    List<Restaurant> restaurants = _repo.Get();
    // Only return restaurants that or not shutdown or you are the owner of
    List<Restaurant> filtered = restaurants.FindAll(r => r.Shutdown == false || r.OwnerId == userId);
    return filtered;
  }

  internal Restaurant GetOne(int idizzle, string userId)
  {
    Restaurant restaurant = _repo.GetOne(idizzle);
    // NOTE access control for shutdown restaurants that are shutdown
    if (restaurant == null) throw new Exception($"nothing there dawg, {idizzle} was no where to be fizzle.");
    if (restaurant.Shutdown == true && restaurant.OwnerId != userId) throw new Exception($"That restaurant is not open right now, come back later dawg, for rizzle.");

    return restaurant;
  }

  internal Restaurant Update(Restaurant updateData)
  {
    Restaurant original = GetOne(updateData.Id, updateData.OwnerId);
    if (original.OwnerId != updateData.OwnerId) throw new Exception("Nacho restaurant");
    original.Name = updateData.Name ?? original.Name;
    original.Description = updateData.Description ?? original.Description;
    original.coverImg = updateData.coverImg ?? original.coverImg;
    original.Shutdown = updateData.Shutdown ?? original.Shutdown;

    _repo.Update(original);
    return original;
  }
}
