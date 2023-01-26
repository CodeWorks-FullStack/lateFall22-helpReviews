namespace helpReviews.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantsController : ControllerBase
{

  private readonly Auth0Provider _auth;
  private readonly RestaurantsService _restaurantsService;
  private readonly ReportsService _reportService;

  public RestaurantsController(Auth0Provider auth, RestaurantsService restaurantsService, ReportsService reportService)
  {
    _auth = auth;
    _restaurantsService = restaurantsService;
    _reportService = reportService;
  }

  [HttpGet]

  public async Task<ActionResult<List<Restaurant>>> Get()
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      // NOTE need to use the ? in cases where you ares using userInfo but not authorizing the route
      List<Restaurant> restaurants = _restaurantsService.Get(userInfo?.Id);
      return Ok(restaurants);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{idizzle}")]
  public async Task<ActionResult<Restaurant>> Get(int idizzle)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      // NOTE need to use the ? in cases where you ares using userInfo but not authorizing the route
      Restaurant restaurant = _restaurantsService.GetOne(idizzle, userInfo?.Id);
      return Ok(restaurant);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{id}/reports")]
  public async Task<ActionResult<List<Report>>> GetReports(int id)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      List<Report> reports = _reportService.GetReports(id, userInfo?.Id);
      return Ok(reports);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Restaurant>> Create([FromBody] Restaurant restaurantData)
  {
    try 
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      restaurantData.OwnerId = userInfo.Id;
      Restaurant restaurant = _restaurantsService.Create(restaurantData);
      return Ok(restaurant); 
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }



  [HttpPut("{id}")]
  [Authorize]
  public async Task<ActionResult<Restaurant>> Update(int id, [FromBody] Restaurant updateData)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      updateData.OwnerId = userInfo.Id;
      updateData.Id = id;
      Restaurant restaurant = _restaurantsService.Update(updateData);
      return Ok(restaurant);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
