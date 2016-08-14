using System;
using System.Collections.Generic;
using System.Linq;
using SugarTracker.Web.DataContext;
using SugarTracker.Web.Entities;

namespace SugarTracker.Web.Services.Repositories
{
  public interface  IReadingRepository
  {
    void AddReading(Reading reading);
    IEnumerable<Reading> GetAllReadings();
    IEnumerable<Reading> GetReadings(DateTime start, DateTime end);
    IEnumerable<Reading> GetReadings(string userId, DateTime start, DateTime end);
    IEnumerable<Reading> GetUserReadings(string userId);
  }

  public class ReadingRepository: IReadingRepository
  {
    private readonly SugarTrackerDbContext _context;

    public ReadingRepository(SugarTrackerDbContext context)
    {
      _context = context;
    }

    public void AddReading(Reading reading)
    {
      _context.Add(reading);
      _context.SaveChanges();
    }

    public IEnumerable<Reading> GetAllReadings()
    {
      throw new NotImplementedException();
    }

    public IEnumerable<Reading> GetReadings(DateTime start, DateTime end)
    {
      var readings = _context.Readings.Where(r => r.ReadingTime >= start.Date && r.ReadingTime <= end.Date);

      foreach (var reading in readings) // specifiy that the kind is Utc
      {
        reading.ReadingTime = DateTime.SpecifyKind(reading.ReadingTime, DateTimeKind.Utc);
      }

      return readings;
    }

    public IEnumerable<Reading> GetReadings(string userId, DateTime start, DateTime end)
    {
      return _context.Readings.Where(r => r.UserId == userId && r.ReadingTime >= start && r.ReadingTime <= end);
    }

    public IEnumerable<Reading> GetUserReadings(string userId)
    {
      return _context.Readings.Where(r => r.UserId == userId);
    }
  }
}