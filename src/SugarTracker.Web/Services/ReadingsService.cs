using System;
using System.Collections.Generic;
using System.Linq;
using SugarTracker.Web.Entities;
using SugarTracker.Web.Services.Repositories;

namespace SugarTracker.Web.Services
{
  public interface IReadingsService
  {
    IEnumerable<Reading> GetReadings(string phoneNumber);
    IEnumerable<RawReading> GetRawReadings();

    Reading SaveReading(RawReading rawReading);
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
  }
}
