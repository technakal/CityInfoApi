using AutoMapper;
using CityInfo.API.Contexts;
using CityInfo.API.Repositories;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CityInfo.API
{
  public class Startup
  {
    private readonly IConfiguration configuration;

    public Startup(IConfiguration configuration)
    {
      this.configuration = configuration ?? throw new System.ArgumentNullException(nameof(configuration));
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo 
        { 
          Title = "CityInfo", 
          Version = "v1", 
          Description = "Returns some stuff about cities.",
          Contact = new OpenApiContact
          {
            Name = "Noel Keener",
            Email = string.Empty,
            Url = new Uri("https://github.com/technakal")
          }
        });

        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
        c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
      });

      services.AddMvc()
        // allows you to specify input and output formatters, to accommodate multiple request/response types.
        .AddMvcOptions(o =>
        {
          o.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
        });
      // allows setting naming conventions on JSON serialization.
      //.AddJsonOptions(o =>
      //{
      //  if(o.SerializerSettings.ContractResolver != null)
      //  {
      //    var castedResolver = o.SerializerSettings.ContractResolver as DefaultContractResolver;
      //    castedResolver.NamingStrategy = null;
      //  }
      //});

#if DEBUG
      services.AddTransient<IMailService, LocalMailService>();
#else
      services.AddTransient<IMailService, CloudMailService>();
#endif

      var connectionString = configuration["connectionStrings:cityInfoDBConnectionString"];
      services.AddDbContext<CityInfoContext>(o => o.UseSqlServer(connectionString));

      services.AddScoped<ICityInfoRepository, CityInfoRepository>();
      services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler();
      }

      app.UseSwagger();
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "CityInfo v1");
      });

      app.UseStatusCodePages();
      app.UseMvc();
    }
  }
}
