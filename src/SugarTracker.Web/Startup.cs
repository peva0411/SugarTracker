using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SugarTracker.Web.DataContext;
using SugarTracker.Web.Entities;
using SugarTracker.Web.Services;
using SugarTracker.Web.Services.Repositories;

namespace SugarTracker.Web
{
  public class Startup
  {

    public IConfiguration Configuration { get; }

    public Startup(IHostingEnvironment env)
    {
      var builder = new ConfigurationBuilder()
        .SetBasePath(env.ContentRootPath)
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .AddEnvironmentVariables();

      if (env.IsDevelopment())
      {
        builder.AddUserSecrets();
      }

      Configuration = builder.Build();
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      var accountSid = Configuration["Twilio.AccountSid"];
      var authToken = Configuration["Twilio.AuthToken"];
      var fromPhone = Configuration["Twilio.FromPhone"];

      if (string.IsNullOrEmpty(accountSid)) throw new Exception("Could not load app settings");

      services.AddMvc()
        .AddXmlSerializerFormatters()
        .AddXmlDataContractSerializerFormatters();

      services.AddDbContext<SugarTrackerDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
      services.AddTransient<IRawReadingsRepository, RawReadingsRepository>();
      services.AddTransient<IReadigRepository, ReadingRepository>();
      services.AddTransient<IReadingParser, ReadingParser>();
      services.AddTransient<IUserPhoneLookupService, UserPhoneLookupService>();
      services.AddTransient<IUserPhoneNumberRepository, UserPhoneNumberRepository>();
      services.AddTransient<IReadingsService, ReadingsService>();

      services.AddTransient<ISmsService, TwilioSmsService>(
        provider => new TwilioSmsService(accountSid, authToken, fromPhone));

      services.AddIdentity<User, IdentityRole>()
        .AddEntityFrameworkStores<SugarTrackerDbContext>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment environment, ILoggerFactory loggerFactory)
    {
      loggerFactory.AddConsole(LogLevel.Debug);
      loggerFactory.AddDebug();

      //if (environment.IsDevelopment())
      //{
        app.UseDeveloperExceptionPage();
      //}
      app.UseIdentity();
      app.UseMvcWithDefaultRoute();
      app.Run(async (context) =>
      {
        await context.Response.WriteAsync("Page not found");
      });
    }
  }
}
