using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using SugarTracker.Web.Entities;
using SugarTracker.Web.Models;
using Twilio.Mvc;

namespace SugarTracker.Web.Services.SmsCommands
{
  public class ReadingsReportCommand : ISmsCommand
  {
    private readonly IReadingsService _readingsService;

    public ReadingsReportCommand(IReadingsService readingsService)
    {
      _readingsService = readingsService;
    }

    public IActionResult Execute(SmsRequest smsRequest)
    {
      var readings = _readingsService.GetReadings(smsRequest.From);
      var responseMessage = FormartPastReadings(readings);
      return new OkObjectResult(new Response() { Message = responseMessage });
    }

    private string FormartPastReadings(IEnumerable<Reading> readings)
    {
      var sb = new StringBuilder();

      sb.AppendLine($"Past {readings.Count()} readings:");
      foreach (var reading in readings)
      {
        sb.AppendLine($"Time {reading.ReadingTime.ToLocalTime(): MM-dd hh:mm tt}, Value: {reading.Value} mg/dL");
      }

      return sb.ToString();
    }
  }
}