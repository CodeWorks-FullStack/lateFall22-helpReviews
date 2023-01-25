using helpReviews.Interfaces;

namespace helpReviews.Repositories;

public class ReportsRepository : IRepository<Report, int>
{

  private readonly IDbConnection _db;

  public ReportsRepository(IDbConnection db)
  {
    _db = db;
  }

  public Report Create(Report reportData)
  {
    string squizzle = @"
    INSERT INTO reports
    (title, body, creatorId, restaurantId)
    VALUES
    (@title, @body, @creatorId, @restaurantId);
    SELECT LAST_INSERT_ID();
    ";
    int id = _db.ExecuteScalar<int>(squizzle, reportData);
    reportData.Id = id;
    return reportData;
  }

  public Report Create()
  {
    throw new NotImplementedException();
  }

  public bool Delete(int id)
  {
    string sql = @"
    DELETE FROM reports
    WHERE id = @id;
    ";
    int rows = _db.Execute(sql, new { id });
    return rows > 0;
  }

  public List<Report> Get()
  {
    throw new NotImplementedException();
  }

  public Report GetOne(int id)
  {
    string sql = @"
    SELECT
    r.*,
    a.*
    FROM reports r
    JOIN accounts a ON r.creatorId = a.id
    WHERE r.id = @id;
    ";
    Report report = _db.Query<Report, Profile, Report>(sql, (report, profile) =>
    {
      report.Creator = profile;
      return report;
    }, new { id }).FirstOrDefault();
    return report;
  }

  public bool Update(Report update)
  {
    throw new NotImplementedException();
  }

  internal List<Report> GetReports(int restaurantId)
  {
    string sql = @"
    SELECT
    r.*,
    a.*
    FROM reports r
    JOIN accounts a ON r.creatorId = a.id
    WHERE r.restaurantId = @restaurantId;
    ";
    List<Report> reports = _db.Query<Report, Profile, Report>(sql, (report, profile) =>
    {
      report.Creator = profile;
      return report;
    }, new { restaurantId }).ToList();
    return reports;
  }
}
