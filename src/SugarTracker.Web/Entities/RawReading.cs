using System;

namespace SugarTracker.Web.Entities
{
    public class RawReading
    {
    public int RawReadingId { get; set; }
    public string Message { get; set; }
    public DateTime ReadingTime { get; set; }
    public string FromPhoneNumber { get; set; }
  }
}
