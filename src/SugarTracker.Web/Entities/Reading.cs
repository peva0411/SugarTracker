using System;

namespace SugarTracker.Web.Entities
{
  public class Reading
    {
    public int ReadingId { get; set; }
    public double Value { get; set; }
    public DateTime ReadingTime { get; set; }
    public int UserId { get; set; }
    public string Notes { get; set; }
    public int RawReadingId { get; set; }

    public ReadingType Type { get; set; }
  }

  public enum ReadingType
  {
    Fasting,
    Meal
  }
}
