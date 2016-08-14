using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SugarTracker.Web.Entities;
using SugarTracker.Web.Services;
using SugarTracker.Web.Services.Repositories;

namespace SugarTracker.Web.Controllers.Api
{
    [Route("api/[Controller]")]
    public class ReadingsController : Controller
    {
      private readonly IReadingsService _readingService;
      private readonly IReadingRepository _readingRepository;
      private readonly UserManager<User> _userManager;

      public ReadingsController(IReadingsService readingService, IReadingRepository readingRepository, UserManager<User> userManager )
      {
        _readingService = readingService;
        _readingRepository = readingRepository;
        _userManager = userManager;
      }

      [Route("")]
      [Authorize]
      public IEnumerable<RawReading> Get()
      {
        return _readingService.GetRawReadings();
      }

      [HttpPost]
      [Route("")]
      public IActionResult Post([FromBody]Reading reading)
      {

        var id = _userManager.GetUserId(HttpContext.User);
        reading.UserId = id;
        _readingRepository.AddReading(reading);
        
        return Ok();
      } 
     
    }
}
