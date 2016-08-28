using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office.CoverPageProps;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SugarTracker.Web.Entities;
using SugarTracker.Web.Services;
using SugarTracker.Web.Services.Repositories;
using SugarTracker.Web.ViewModels;

namespace SugarTracker.Web.Controllers.Api
{
    [Route("api/[Controller]")]
    [Authorize]
    public class WeekViewController : Controller
    {
      private readonly IReadingsService _readingsService;
      private readonly UserManager<User> _userManager;

      public WeekViewController(IReadingsService readingsService, UserManager<User> userManager )
      {
        _readingsService = readingsService;
        _userManager = userManager;
      }

      [HttpGet("{date:datetime}")]
      public async Task<IActionResult> Get(DateTime date)
      {
        var user = await _userManager.GetUserAsync(HttpContext.User);

        var weekViewModel = _readingsService.GetFormattedWeek(user.Id, date);

        return Ok(weekViewModel);
      }
    }

  public class WeekViewModel
  {
    public IList<ReadingViewModel> Days { get; set; }

    public string UserId { get; set; }

    public WeekViewModel()
    {
      Days = new List<ReadingViewModel>();
    }
  }
  
  public class ReadingViewModel
  {
    public DateTime Date { get; set; }
    public string DayOfWeek { get; set; }
    public Reading Fasting { get; set; }
    public Reading Breakfast { get; set; }
    public Reading Lunch { get; set; }
    public Reading Dinner { get; set; }

    public IList<Reading> AdHocReadings { get; set; }

    public ReadingViewModel()
    {
      AdHocReadings = new List<Reading>();
    } 
  }
}
