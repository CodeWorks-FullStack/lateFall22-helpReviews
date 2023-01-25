using helpReviews.Interfaces;

namespace helpReviews.Repositories;

public class RestaurantsRepository : IRepository<Restaurant, int>
{
  private readonly IDbConnection _db;

  public RestaurantsRepository(IDbConnection db)
  {
    _db = db;
  }

  public Restaurant Create(Restaurant restaurantData)
  {
    throw new NotImplementedException();
  }

  public bool Delete(int id)
  {
    throw new NotImplementedException();
  }

  public List<Restaurant> Get()
  {
    string sql = @"
    SELECT
    *
    FROM restaurants;
    ";
    List<Restaurant> restaurants = _db.Query<Restaurant>(sql).ToList();
    return restaurants;
  }

  public Restaurant GetOne(int id)
  {
    string sql = @"
    SELECT
    *
    FROM restaurants
    WHERE id = @id;
    ";
    Restaurant restaurant = _db.Query<Restaurant>(sql, new { id }).FirstOrDefault();
    return restaurant;
  }

  public bool Update(Restaurant update)
  {
    string sql = @"
      UPDATE restaurants SET
      name = @name,
      description = @description,
      coverImg = @coverImg,
      exposure = @exposure,
      shutdown = @shutdown
      WHERE id = @id;
    ";
    int rows = _db.Execute(sql, update);
    return rows > 0;
  }
}
