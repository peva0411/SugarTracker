using System.Collections.Generic;
using SugarTracker.Web.DataContext;
using SugarTracker.Web.Entities;

namespace SugarTracker.Web.Services.Repositories
{
  public interface IRawReadingsRepository
  {
    void AddRawReading(RawReading rawReading);
    IEnumerable<RawReading> GetAllRawReadings();
  }

  public class RawReadingsRepository : IRawReadingsRepository
  {
    private readonly SugarTrackerDbContext _sugarTrackerDbContext;

    public RawReadingsRepository(SugarTrackerDbContext sugarTrackerDbContext)
    {
      _sugarTrackerDbContext = sugarTrackerDbContext;
    }

    public void AddRawReading(RawReading rawReading)
    {
      _sugarTrackerDbContext.Add(rawReading);
      _sugarTrackerDbContext.SaveChanges();
    }

    public IEnumerable<RawReading> GetAllRawReadings()
    {
      return _sugarTrackerDbContext.RawReadings;
    } 
  }
}
