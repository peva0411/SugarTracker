using Microsoft.AspNetCore.Mvc;
using Twilio.Mvc;

namespace SugarTracker.Web.Services.SmsCommands
{
  public interface ISmsCommand
  {
    IActionResult Execute(SmsRequest smsRequest);
  }
}