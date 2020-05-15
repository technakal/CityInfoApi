using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;

namespace CityInfo.API.Services
{
  public class CloudMailService : IMailService
  {
    private readonly IConfiguration configuration;

    public CloudMailService(IConfiguration configuration)
    {
      this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public void Send(string subject, string message)
    {
      Debug.WriteLine($"Mail from {configuration["mailSettings:mailToAddress"]} to {configuration["mailSettings:mailFromAddress"]} with CloudMailService");
      Debug.WriteLine($"Subject: {subject}");
      Debug.WriteLine($"Message: {message}");
    }
  }
}
