using CityInfo.API.Models;
using System.Collections.Generic;

namespace CityInfo.API
{
  public class CitiesDataStore
  {
    public static CitiesDataStore Current { get; } = new CitiesDataStore();

    public List<CityDto> Cities { get; set; }

    public CitiesDataStore()
    {
      Cities = new List<CityDto>()
      {
        new CityDto()
        {
          Id = 1,
          Name = "New York City",
          Description = "The big Apple.",
          PointsOfInterest = new List<PointOfInterestDto>()
          {
            new PointOfInterestDto()
            {
              Id = 1,
              Name = "Central Park",
              Description = "The most visited urban park in the United States."
            },
            new PointOfInterestDto()
            {
              Id = 2,
              Name = "Empire State Building",
              Description = "A 102-story skyscraper located in Midtown Manhattan."
            }
          }
        },
        new CityDto()
        {
          Id = 2,
          Name = "Des Moines",
          Description = "Top 10 for new COVID cases!",
          PointsOfInterest = new List<PointOfInterestDto>()
          {
            new PointOfInterestDto()
            {
              Id = 1,
              Name = "Capitol Building",
              Description = "The largest golden dome of the Midwest."
            },
            new PointOfInterestDto()
            {
              Id = 2,
              Name = "Gray's Lake Park",
              Description = "Beautiful trails surrounding an idyllic lake."
            }
          }
        },
        new CityDto()
        {
          Id = 3,
          Name = "Pierre",
          Description = "The capital of one of the lesser states.",
          PointsOfInterest = new List<PointOfInterestDto>()
          {
            new PointOfInterestDto()
            {
              Id = 1,
              Name = "Cultural Heritage Center",
              Description = "Rolling hills, yo."
            },
            new PointOfInterestDto()
            {
              Id = 2,
              Name = "Flaming Fountain",
              Description = "It's not actually on fire."
            }
          }
        }
      };
    }
  }
}
