using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CityInfo.API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class CitiesController : ControllerBase
  {
    private readonly ICityInfoRepository cityInfoRepository;
    private readonly IMapper mapper;

    public CitiesController(ICityInfoRepository cityInfoRepository, IMapper mapper)
    {
      this.cityInfoRepository = cityInfoRepository ?? throw new System.ArgumentNullException(nameof(cityInfoRepository));
      this.mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
    }

    /// <summary>
    /// Retrieves all cities in the database.
    /// </summary>
    /// <returns>A list of cities or an empty array.</returns>
    /// <response code="200">Returns all cities, or an empty array.</response>
    [HttpGet]
    public IActionResult GetCities()
    {
      var cityEntities = cityInfoRepository.GetCities();
      return Ok(mapper.Map<IEnumerable<CityWithoutPointsOfInterestDto>>(cityEntities));
    }

    /// <summary>
    /// Retrieves a specific city, by id.
    /// Optionally returns the points of interest of that city.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="includePointsOfInterest"></param>
    /// <returns>A single city.</returns>
    /// <response code="200">Returns the city and a list of the points of interest, if requested.</response>
    /// <response code="404">If no city matching the id is found.</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetCity(int id, bool includePointsOfInterest = false)
    {
      var city = cityInfoRepository.GetCity(id, includePointsOfInterest);
      if (city == null)
      {
        return NotFound($"No city matches id {id}.");
      }
      if (includePointsOfInterest)
      {
        return Ok(mapper.Map<CityDto>(city));
      }
      return Ok(mapper.Map<CityWithoutPointsOfInterestDto>(city));
    }
  }
}
