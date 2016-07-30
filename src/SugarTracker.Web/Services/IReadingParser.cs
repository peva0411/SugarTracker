using System;
using SugarTracker.Web.Entities;

namespace SugarTracker.Web.Services
{
  public interface IReadingParser
  {
    Reading ParseRawReading(RawReading rawReading);
  }

  public class ReadingParser : IReadingParser
  {
    public Reading ParseRawReading(RawReading rawReading)
    {
      var reading = new Reading();

      var bodyParts = rawReading.Message.Split(',');
      if (bodyParts.Length < 2) throw new InvalidReadingException("This reading is missing information: type[f,b,l,d,a],value,notes[optional]");
 
      reading.Type = GetReadingType(bodyParts[0]);
      reading.Value = double.Parse(bodyParts[1]);

      if (bodyParts.Length == 3)
        reading.Notes = bodyParts[2];

      return reading;
    }

    private ReadingType GetReadingType(string typeCode)
    {
      switch (typeCode.ToLower())
      {
        case "f":
           return ReadingType.Fasting;
        case "b":
          return ReadingType.Breakfast;
        case "l":
          return ReadingType.Lunch;
        case "d":
          return ReadingType.Dinner;
        default:
          return ReadingType.AdHoc;
      }
    }
  }

  public class InvalidReadingException : Exception
  {
    public InvalidReadingException(string message):base(message)
    {
    }
  }
}