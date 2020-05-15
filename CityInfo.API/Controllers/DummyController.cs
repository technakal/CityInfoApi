using CityInfo.API.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CityInfo.API.Controllers
{
  [ApiController]
  [Route("api/testdatabase")]
  public class DummyController : ControllerBase
  {
    private readonly CityInfoContext ctx;

    public DummyController(CityInfoContext ctx)
    {
      this.ctx = ctx ?? throw new ArgumentNullException(nameof(ctx));
    }

    /// <summary>
    /// Tests whether a database connection exists.
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Indicates a connection was established.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult TestDatabase()
    {
      return Ok();
    }
  }
}
