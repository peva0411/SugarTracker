using System;
using SugarTracker.Web.Entities;
using Twilio.Mvc;

namespace SugarTracker.Web.Services
{
  public static class SmsRequestExtentions
  {
    public static RawReading ToRawReading(this SmsRequest request)
    {
      return new RawReading
      {
        Message = request.Body,
        FromPhoneNumber = request.From,
        ReadingTime = DateTime.UtcNow
      };
    }
  }
}