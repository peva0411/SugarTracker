using Twilio.Mvc;

namespace SugarTracker.Web.Services.SmsCommands
{
  public static class SmsCommandFactory
  {
    public static ISmsCommand GetCommand(IReadingsService readingsService, SmsRequest smsRequest)
    {
      if (smsRequest.Body.ToLower() == "readings")
      {
        return new ReadingsReportCommand(readingsService);
      }
      else
      {
        return new ReadingSaveCommand(readingsService);
      }
    }
  }
}