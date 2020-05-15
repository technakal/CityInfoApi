using CityInfo.API.Contexts;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NLog.Web;
using System;

namespace CityInfo.API
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var logger = NLogBuilder
        .ConfigureNLog("nlog.config")
        .GetCurrentClassLogger();

      try
      {
        logger.Info("Initializing application...");
        var host = CreateWebHostBuilder(args).Build();

        using (var scope = host.Services.CreateScope())
        {
          try
          {
            var context = scope.ServiceProvider.GetService<CityInfoContext>();

            // for demo purposes only, delete the database to give us a clean state. Don't ever do this in PROD.
            // context.Database.EnsureDeleted();

            context.Database.Migrate();
          }
          catch (Exception exc)
          {
            logger.Error(exc, "An error occurred while migrating the database.");
          }
        }

        host.Run();
      }
      catch (Exception exc)
      {
        logger.Error(exc, "Application failed in startup.");
        throw;
      }
      finally
      {
        NLog.LogManager.Shutdown();
      }
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .UseNLog();
  }
}
