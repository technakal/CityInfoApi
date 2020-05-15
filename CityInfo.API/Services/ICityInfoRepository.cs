using CityInfo.API.Entities;
using System.Collections.Generic;

namespace CityInfo.API.Services
{
  public interface ICityInfoRepository
  {
    IEnumerable<City> GetCities();
    City GetCity(int cityId, bool incluePointsOfInterest);
    IEnumerable<PointOfInterest> GetPointsOfInterestForCity(int cityId);
    PointOfInterest GetPointOfInterestForCity(int cityId, int pointOfInterestId);
    bool CityExists(int cityId);
    bool PointOfInterestExists(int id);
    void AddPointOfInterestForCity(int cityId, PointOfInterest pointOfInterest);
    void UpdatePointOfInterestForCity(int cityId, PointOfInterest pointOfInterest);
    void DeletePointOfInterestForCity(PointOfInterest pointOfInterest);
    bool Save();
  }
}
