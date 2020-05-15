using CityInfo.API.Contexts;
using CityInfo.API.Entities;
using CityInfo.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.ObjectPool;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CityInfo.API.Repositories
{
  public class CityInfoRepository : ICityInfoRepository
  {
    private readonly CityInfoContext context;

    public CityInfoRepository(CityInfoContext context)
    {
      this.context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public IEnumerable<City> GetCities()
    {
      return context.Cities.OrderBy(c => c.Name).ToList();
    }

    public City GetCity(int cityId, bool includePointsOfInterest)
    {
      if (includePointsOfInterest)
      {
        return context.Cities.Include(c => c.PointsOfInterest)
          .Where(c => c.Id == cityId).FirstOrDefault();
      }
      return context.Cities.Where(c => c.Id == cityId).FirstOrDefault();
    }

    public PointOfInterest GetPointOfInterestForCity(int cityId, int pointOfInterestId)
    {
      return context.PointsOfInterest
        .Where(p => p.CityId == cityId && p.Id == pointOfInterestId)
        .FirstOrDefault();
    }

    public IEnumerable<PointOfInterest> GetPointsOfInterestForCity(int cityId)
    {
      return context.PointsOfInterest
        .Where(p => p.CityId == cityId).ToList();
    }

    public bool CityExists(int cityId)
    {
      return context.Cities.Any(c => c.Id == cityId);
    }

    public void AddPointOfInterestForCity(int cityId, PointOfInterest pointOfInterest)
    {
      var city = GetCity(cityId, false);
      city.PointsOfInterest.Add(pointOfInterest);
    }

    public void UpdatePointOfInterestForCity(int cityId, PointOfInterest pointOfInterest)
    {

    }

    public bool Save()
    {
      return context.SaveChanges() >= 0;
    }

    public bool PointOfInterestExists(int id)
    {
      return context.PointsOfInterest.Any(p => p.Id == id);
    }

    public void DeletePointOfInterestForCity(PointOfInterest pointOfInterest)
    {
      context.PointsOfInterest.Remove(pointOfInterest);
    }
  }
}
