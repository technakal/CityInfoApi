using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.Contexts
{
  public class CityInfoContext : DbContext
  {
    public DbSet<City> Cities { get; set; }
    public DbSet<PointOfInterest> PointsOfInterest { get; set; }

    public CityInfoContext(DbContextOptions<CityInfoContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<City>()
        .HasData(
          new City()
          {
            Id = 1,
            Name = "New York City",
            Description = "The big Apple.",
          },
          new City()
          {
            Id = 2,
            Name = "Des Moines",
            Description = "Top 10 for new COVID cases!",
          },
          new City()
          {
            Id = 3,
            Name = "Pierre",
            Description = "The capital of one of the lesser states.",
          }
        );

      modelBuilder.Entity<PointOfInterest>()
        .HasData(
          new PointOfInterest()
          {
            Id = 1,
            CityId = 1,
            Name = "Central Park",
            Description = "The most visited urban park in the United States."
          },
          new PointOfInterest()
          {
            Id = 2,
            CityId = 1,
            Name = "Empire State Building",
            Description = "A 102-story skyscraper located in Midtown Manhattan."
          },
          new PointOfInterest()
          {
            Id = 3,
            CityId = 2,
            Name = "Capitol Building",
            Description = "The largest golden dome of the Midwest."
          },
          new PointOfInterest()
          {
            Id = 4,
            CityId = 2,
            Name = "Gray's Lake Park",
            Description = "Beautiful trails surrounding an idyllic lake."
          },
          new PointOfInterest()
          {
            Id = 5,
            CityId = 3,
            Name = "Cultural Heritage Center",
            Description = "Rolling hills, yo."
          },
          new PointOfInterest()
          {
            Id = 6,
            CityId = 3,
            Name = "Flaming Fountain",
            Description = "It's not actually on fire."
          }
        );
      base.OnModelCreating(modelBuilder);
    }
  }
}