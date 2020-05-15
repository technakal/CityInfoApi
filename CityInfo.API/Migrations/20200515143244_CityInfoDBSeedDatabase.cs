using Microsoft.EntityFrameworkCore.Migrations;

namespace CityInfo.API.Migrations
{
  public partial class CityInfoDBSeedDatabase : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.InsertData(
          table: "Cities",
          columns: new[] { "Id", "Description", "Name" },
          values: new object[] { 1, "The big Apple.", "New York City" });

      migrationBuilder.InsertData(
          table: "Cities",
          columns: new[] { "Id", "Description", "Name" },
          values: new object[] { 2, "Top 10 for new COVID cases!", "Des Moines" });

      migrationBuilder.InsertData(
          table: "Cities",
          columns: new[] { "Id", "Description", "Name" },
          values: new object[] { 3, "The capital of one of the lesser states.", "Pierre" });

      migrationBuilder.InsertData(
          table: "PointsOfInterest",
          columns: new[] { "Id", "CityId", "Description", "Name" },
          values: new object[,]
          {
                    { 1, 1, "The most visited urban park in the United States.", "Central Park" },
                    { 2, 1, "A 102-story skyscraper located in Midtown Manhattan.", "Empire State Building" },
                    { 3, 2, "The largest golden dome of the Midwest.", "Capitol Building" },
                    { 4, 2, "Beautiful trails surrounding an idyllic lake.", "Gray's Lake Park" },
                    { 5, 3, "Rolling hills, yo.", "Cultural Heritage Center" },
                    { 6, 3, "It's not actually on fire.", "Flaming Fountain" }
          });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DeleteData(
          table: "PointsOfInterest",
          keyColumn: "Id",
          keyValue: 1);

      migrationBuilder.DeleteData(
          table: "PointsOfInterest",
          keyColumn: "Id",
          keyValue: 2);

      migrationBuilder.DeleteData(
          table: "PointsOfInterest",
          keyColumn: "Id",
          keyValue: 3);

      migrationBuilder.DeleteData(
          table: "PointsOfInterest",
          keyColumn: "Id",
          keyValue: 4);

      migrationBuilder.DeleteData(
          table: "PointsOfInterest",
          keyColumn: "Id",
          keyValue: 5);

      migrationBuilder.DeleteData(
          table: "PointsOfInterest",
          keyColumn: "Id",
          keyValue: 6);

      migrationBuilder.DeleteData(
          table: "Cities",
          keyColumn: "Id",
          keyValue: 1);

      migrationBuilder.DeleteData(
          table: "Cities",
          keyColumn: "Id",
          keyValue: 2);

      migrationBuilder.DeleteData(
          table: "Cities",
          keyColumn: "Id",
          keyValue: 3);
    }
  }
}
