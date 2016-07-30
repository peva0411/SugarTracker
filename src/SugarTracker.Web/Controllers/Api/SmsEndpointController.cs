using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SugarTracker.Web.Services;
using SugarTracker.Web.Services.SmsCommands;
using Twilio.Mvc;

namespace SugarTracker.Web.Controllers.Api
{
  [Route("api/[controller]")]
  public class SmsEndpointController : Controller
  {
    private readonly IReadingsService _readingsService;

    public SmsEndpointController(IReadingsService readingsService)
    {
      _readingsService = readingsService;
    }

    [AllowAnonymous]
    [Route("[Action]")]
    [Produces("application/xml")]
    public IActionResult AddReadingFromSms(SmsRequest request)
    {
      var command = SmsCommandFactory.GetCommand(_readingsService, request);
      return command.Execute(request);
    }
  }
}
