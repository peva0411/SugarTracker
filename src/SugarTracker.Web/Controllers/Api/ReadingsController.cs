using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SugarTracker.Web.Entities;
using SugarTracker.Web.Services;

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

      [Route("")]
      [Authorize]
      public IEnumerable<RawReading> Get()
      {
        return _readingService.GetRawReadings();
      } 
    }
}
