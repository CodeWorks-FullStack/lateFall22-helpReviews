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
    string sql = @"
      INSERT INTO restaurants
        (name, `ownerId`, `coverImg`, description)
      VALUES
        (@Name, @OwnerId, @CoverImg, @Description);
      SELECT LAST_INSERT_ID()
      ;";

    restaurantData.Id = _db.ExecuteScalar<int>(sql, restaurantData);
    return restaurantData;
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
    r.*,
    COUNT(rep.id) AS ReportCount
    FROM restaurants r
    JOIN reports rep ON r.id = rep.restaurantId
    WHERE r.id = @id;
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
