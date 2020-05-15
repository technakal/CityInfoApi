using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;

namespace CityInfo.API.Services
{
  public class LocalMailService : IMailService
  {
    private readonly IConfiguration configuration;

    public LocalMailService(IConfiguration configuration)
    {
      this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public void Send(string subject, string message)
    {
      Debug.WriteLine($"Mail from {configuration["mailSettings:mailFromAddress"]} to {configuration["mailSettings:mailToAddress"]} with LocalMailService");
      Debug.WriteLine($"Subject: {subject}");
      Debug.WriteLine($"Message: {message}");
    }
  }
}
