namespace helpReviews.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
  private readonly Auth0Provider _auth;
  private readonly ReportsService _reportsService;

  public ReportsController(Auth0Provider auth, ReportsService reportsService)
  {
    _auth = auth;
    _reportsService = reportsService;
  }

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Report>> Create([FromBody] Report reportData)
  {
    try
    {
      // NOTE Profile or account will work here, no need to hide the email from the person who is making it.
      Profile userInfo = await _auth.GetUserInfoAsync<Profile>(HttpContext);
      // NOTE assign the creator id from userInfo
      reportData.CreatorId = userInfo.Id;
      Report report = _reportsService.Create(reportData);
      // NOTE populate the creator back onto the report for a create
      reportData.Creator = userInfo;
      return Ok(report);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpDelete("{id}")]
  [Authorize]
  public async Task<ActionResult<string>> Remove(int id)
  {
    try
    {
      Profile userInfo = await _auth.GetUserInfoAsync<Profile>(HttpContext);
      string message = _reportsService.Remove(id, userInfo.Id);
      return Ok(message);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
