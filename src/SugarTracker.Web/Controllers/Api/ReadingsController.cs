using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SugarTracker.Web.Entities;
using SugarTracker.Web.Models;
using SugarTracker.Web.Services;
using Twilio.Mvc;

namespace SugarTracker.Web.Controllers.Api
{
    [Route("api/[Controller]")]
    public class ReadingsController : Controller
    {
      private readonly IReadingsService _readingService;

      public ReadingsController(IReadingsService readingService)
      {
        _readingService = readingService;
      }

      [Route("[Action]")]
      [Produces("application/xml")]
      public Response AddReadingFromSms(SmsRequest request)
      {
        _readingService.SaveRawReading(new RawReading() {Message = request.Body, FromPhoneNumber = request.From, ReadingTime = DateTime.Now});
        return new Response() {Message = $"Added value: {request.Body}" };
      }

      [Route("")]
      [Authorize]
      public IEnumerable<RawReading> Get()
      {
        return _readingService.GetRawReadings();
      } 

    }
}
