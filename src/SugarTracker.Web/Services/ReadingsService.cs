using System;
using System.Collections.Generic;
using SugarTracker.Web.Entities;
using SugarTracker.Web.Services.Repositories;

namespace SugarTracker.Web.Services
{
  public interface IReadingsService
  {
    IEnumerable<Reading> GetReadings();
    IEnumerable<RawReading> GetRawReadings();

    void SaveRawReading(RawReading rawReading);
  }

  public class ReadingsService : IReadingsService
  {
    private readonly IRawReadingsRepository _rawReadingsRepository;

    public ReadingsService(IRawReadingsRepository rawReadingsRepository)
    {
      _rawReadingsRepository = rawReadingsRepository;
    }

    public IEnumerable<Reading> GetReadings()
    {
      return new List<Reading>
      {
        new Reading()
        {
          ReadingId = 1,
          ReadingTime = DateTime.Now,
          Type = ReadingType.Fasting,
          Value = 120.0,
          Notes = "This is a test",
          UserId = 1
        }
      };
    }

    public IEnumerable<RawReading> GetRawReadings()
    {
      return _rawReadingsRepository.GetAllRawReadings();
    }

    public void SaveRawReading(RawReading rawReading)
    {
      _rawReadingsRepository.AddRawReading(rawReading);
    }
  }
}
