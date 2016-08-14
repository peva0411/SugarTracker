using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office.CoverPageProps;
using Microsoft.AspNetCore.Mvc;
using SugarTracker.Web.Entities;
using SugarTracker.Web.Services.Repositories;
using SugarTracker.Web.ViewModels;

namespace SugarTracker.Web.Controllers.Api
{
    [Route("api/[Controller]")]
    public class WeekViewController : Controller
    {
      private readonly IReadingRepository _readingRepository;

      public WeekViewController(IReadingRepository readingRepository)
      {
        _readingRepository = readingRepository;
      }

      [HttpGet("{date:datetime}")]
      public IActionResult Get(DateTime date)
      {
        var readings =_readingRepository.GetReadings(date, date.AddDays(7)).ToList();
        var weekViewModel = new WeekViewModel();

        //convert utc times to users local timezone --hard coded for now;
        ConvertTimes(readings);

        for (int i = 0; i < 7; i++)
        {
          var currentDay = date.AddDays(i);
         
          var currentDayReadings = readings.Where(r => r.ReadingTime.Date == currentDay.Date).ToList();

          var dayViewModel = new ReadingViewModel();
          dayViewModel.DayOfWeek = currentDay.DayOfWeek.ToString();
          dayViewModel.Date = currentDay;
          dayViewModel.Fasting = currentDayReadings.SingleOrDefault(c => c.Type == ReadingType.Fasting);
          dayViewModel.Breakfast = currentDayReadings.SingleOrDefault(c => c.Type == ReadingType.Breakfast);
          dayViewModel.Lunch = currentDayReadings.SingleOrDefault(c => c.Type == ReadingType.Lunch);
          dayViewModel.Dinner = currentDayReadings.SingleOrDefault(c => c.Type == ReadingType.Dinner);
          dayViewModel.AdHocReadings = currentDayReadings.Where(c => c.Type == ReadingType.AdHoc).ToList();

          weekViewModel.Days.Add(dayViewModel);
        }

        return Ok(weekViewModel);
      }

      private void ConvertTimes(List<Reading> readings)
      {

        var userTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

        foreach (var reading in readings)
        {
          if (reading.ReadingTime.Kind != DateTimeKind.Utc) throw new Exception("Must be in utc to convert to user's timezone");
          reading.ReadingTime = TimeZoneInfo.ConvertTimeFromUtc(reading.ReadingTime, userTimeZone);
        }
      }
    }

  public class WeekViewModel
  {
    public IList<ReadingViewModel> Days { get; set; }

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
