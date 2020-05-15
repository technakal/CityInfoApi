using AutoMapper;
using CityInfo.API.Entities;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CityInfo.API.Controllers
{
  [ApiController]
  [Route("api/cities/{cityId}/pointsofinterest")]
  public class PointsOfInterestController : ControllerBase
  {
    private readonly ILogger<PointsOfInterestController> logger;
    private readonly IMailService mailService;
    private readonly ICityInfoRepository cityInfoRepository;
    private readonly IMapper mapper;

    public PointsOfInterestController(
      ILogger<PointsOfInterestController> logger,
      IMailService mailService,
      ICityInfoRepository cityInfoRepository,
      IMapper mapper)
    {
      this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
      this.mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
      this.cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
      this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetPointsOfInterest(int cityId)
    {
      try
      {
        if (!cityInfoRepository.CityExists(cityId))
        {
          logger.LogInformation($"No city matching id {cityId} was found when accessing point of interest.");
          return NotFound($"No city matching id {cityId}.");
        }

        var pointsOfInterestForCity = cityInfoRepository.GetPointsOfInterestForCity(cityId);

        return Ok(mapper.Map<IEnumerable<PointOfInterestDto>>(pointsOfInterestForCity));
      }
      catch (Exception exc)
      {
        logger.LogCritical($"Exception while getting points of interest for city {cityId}.", exc);
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
    }

    [HttpGet("{id:int}", Name = "GetPointOfInterest")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetPointOfInterest(int cityId, int id)
    {
      if (!cityInfoRepository.CityExists(cityId))
      {
        return NotFound($"No city matching id {cityId}.");
      }

      var pointOfInterest = cityInfoRepository.GetPointOfInterestForCity(cityId, id);
      if (pointOfInterest == null)
      {
        return NotFound($"No point of interest matching id {id}.");
      }

      return Ok(mapper.Map<PointOfInterestDto>(pointOfInterest));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult CreatePointOfInterest(int cityId, [FromBody] PointOfInterestForCreationDto model)
    {
      if (model.Name == model.Description)
      {
        ModelState.AddModelError(
          "Description",
          "The provided description should be different from the name."
          );
      }

      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      if (!cityInfoRepository.CityExists(cityId))
      {
        return NotFound($"No city matching id {cityId}.");
      }

      var pointOfInterest = mapper.Map<PointOfInterestForCreationDto, PointOfInterest>(model);
      
      cityInfoRepository.AddPointOfInterestForCity(cityId, pointOfInterest);
      cityInfoRepository.Save();

      var createdPointOfInterest = mapper.Map<PointOfInterestDto>(pointOfInterest);

      return CreatedAtRoute(
        "GetPointOfInterest", 
        new { cityId, id = createdPointOfInterest.Id }, 
        createdPointOfInterest
      );
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult UpdatePointOfInterest(int cityId, int id, [FromBody] PointOfInterestForUpdateDto model)
    {
      if (model.Name == model.Description)
      {
        ModelState.AddModelError(
          "Description",
          "The provided description should be different from the name."
          );
      }

      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      if (!cityInfoRepository.CityExists(cityId))
      {
        return NotFound($"No city matching id {cityId}.");
      }

      var pointOfInterest = cityInfoRepository.GetPointOfInterestForCity(cityId, id);

      if (!cityInfoRepository.PointOfInterestExists(id))
      {
        return NotFound($"No point of interest matching id {id}.");
      }

      mapper.Map(model, pointOfInterest);

      cityInfoRepository.UpdatePointOfInterestForCity(cityId, pointOfInterest);

      cityInfoRepository.Save();

      return NoContent();
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult PartiallyUpdatePointOfInterest(int cityId, int id, [FromBody] JsonPatchDocument<PointOfInterestForUpdateDto> patchDoc)
    {
      if (!cityInfoRepository.CityExists(cityId))
      {
        return NotFound($"No city matching id {cityId}.");
      }

      var pointOfInterest = cityInfoRepository.GetPointOfInterestForCity(cityId, id);
      if (!cityInfoRepository.PointOfInterestExists(id))
      {
        return NotFound($"No point of interest matching id {id}.");
      }

      var pointOfInterestToPatch = mapper.Map<PointOfInterestForUpdateDto>(pointOfInterest);

      patchDoc.ApplyTo(pointOfInterestToPatch, ModelState);

      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      if (pointOfInterestToPatch.Description == pointOfInterestToPatch.Name)
      {
        ModelState.AddModelError(
          "Description",
          "The provided description should be different from the name."
          );
      }
      if (!TryValidateModel(pointOfInterestToPatch))
      {
        return BadRequest(ModelState);
      }

      mapper.Map(pointOfInterestToPatch, pointOfInterest);

      cityInfoRepository.UpdatePointOfInterestForCity(cityId, pointOfInterest);

      cityInfoRepository.Save();

      return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeletePointOfInterest(int cityId, int id)
    {
      if (!cityInfoRepository.CityExists(cityId))
      {
        return NotFound($"No city matching id {cityId}.");
      }

      var pointOfInterest = cityInfoRepository.GetPointOfInterestForCity(cityId, id);
      if (!cityInfoRepository.PointOfInterestExists(id))
      {
        return NotFound($"No point of interest matching id {id}.");
      }

      cityInfoRepository.DeletePointOfInterestForCity(pointOfInterest);
      cityInfoRepository.Save();

      mailService.Send("Point of interest deleted.", $"Point of interest {pointOfInterest.Name} with id {pointOfInterest.Id} has been deleted.");
      return NoContent();
    }
  }
}
