using AutoMapper;

namespace CityInfo.API.Profiles
{
  public class PointOfInterestProfile : Profile
  {
    public PointOfInterestProfile()
    {
      CreateMap<Entities.PointOfInterest, Models.PointOfInterestDto>().ReverseMap();
      CreateMap<Models.PointOfInterestForCreationDto, Entities.PointOfInterest>().ReverseMap();
      CreateMap<Models.PointOfInterestForUpdateDto, Entities.PointOfInterest>().ReverseMap();
    }
  }
}
