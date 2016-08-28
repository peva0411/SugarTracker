using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using SugarTracker.Web.Controllers.Api;
using SugarTracker.Web.Entities;
using SugarTracker.Web.Services.Repositories;

namespace SugarTracker.Web.Services
{
  public interface IReadingsService
  {
    IEnumerable<Reading> GetReadings(string phoneNumber);
    IEnumerable<RawReading> GetRawReadings();

    Reading SaveReading(RawReading rawReading);

    WeekViewModel GetFormattedWeek(string userId, DateTime startDate);
  }

  public class ReadingsService : IReadingsService
  {
    private readonly IRawReadingsRepository _rawReadingsRepository;
    private readonly IReadingRepository _readingRepository;
    private readonly IReadingParser _readingParser;
    private readonly IUserPhoneLookupService _userPhoneLookup;

    public ReadingsService(IRawReadingsRepository rawReadingsRepository, IReadingRepository readingRepository, IReadingParser readingParser, IUserPhoneLookupService userPhoneLookup)
    {
      _rawReadingsRepository = rawReadingsRepository;
      _readingRepository = readingRepository;
      _readingParser = readingParser;
      _userPhoneLookup = userPhoneLookup;
    }

    public IEnumerable<Reading> GetReadings(string phoneNumber)
    {
      var userId = _userPhoneLookup.LookupUserId(phoneNumber);
      return _readingRepository.GetUserReadings(userId).OrderByDescending(r => r.ReadingTime).Take(6);
    }

    public IEnumerable<RawReading> GetRawReadings()
    {
      return _rawReadingsRepository.GetAllRawReadings();
    }

    public Reading SaveReading(RawReading rawReading)
    {
      //save raw
      rawReading.ReadingTime = DateTime.UtcNow;
      _rawReadingsRepository.AddRawReading(rawReading);

      //convert raw
      var reading = _readingParser.ParseRawReading(rawReading);
      reading.UserId = _userPhoneLookup.LookupUserId(rawReading.FromPhoneNumber);
      reading.RawReadingId = rawReading.RawReadingId;
      reading.ReadingTime = rawReading.ReadingTime;

      //save converted
      _readingRepository.AddReading(reading);

      return reading;
    }

    public WeekViewModel GetFormattedWeek(string userId, DateTime startDate)
    {
      var readings = _readingRepository.GetReadings(userId, startDate, startDate.AddDays(7)).ToList();
      var weekViewModel = new WeekViewModel();

      weekViewModel.UserId = userId;

      //convert utc times to users local timezone --hard coded for now;
      ConvertTimes(readings);

      for (int i = 0; i < 7; i++)
      {
        var currentDay = startDate.AddDays(i);

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

      return weekViewModel;
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
}
