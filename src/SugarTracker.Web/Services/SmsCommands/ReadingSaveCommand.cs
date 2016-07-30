using Microsoft.AspNetCore.Mvc;
using SugarTracker.Web.Controllers.Api;
using SugarTracker.Web.Models;
using Twilio.Mvc;

namespace SugarTracker.Web.Services.SmsCommands
{
  public class ReadingSaveCommand : ISmsCommand
  {
    private readonly IReadingsService _readingsService;

    public ReadingSaveCommand(IReadingsService readingsService)
    {
      _readingsService = readingsService;
    }

    public IActionResult Execute(SmsRequest smsRequest)
    {
      var rawReading = smsRequest.ToRawReading();
      string responseMessage;
      try
      {
        var reading = _readingsService.SaveReading(rawReading);
        responseMessage =
          $"Successfully added reading of {reading.Value} mg/dL at {reading.ReadingTime.ToLocalTime()}";
      }
      catch (InvalidReadingException ex)
      {
        responseMessage = ex.Message;
      }
      return new OkObjectResult(new Response(responseMessage));
    }
  }
}