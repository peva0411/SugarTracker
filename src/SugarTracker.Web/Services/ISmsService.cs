using Twilio;

namespace SugarTracker.Web.Services
{
    public interface ISmsService
    {
      void SendMessage(string toPhone, string message);
    }

    public class TwilioSmsService : ISmsService
    {
      private readonly string _twilioNumber;
      private readonly TwilioRestClient _client;

    public TwilioSmsService(string accountSid, string authToken, string twilioNumber)
    {
      _twilioNumber = twilioNumber;
      _client = new TwilioRestClient(accountSid, authToken);
    }
    
      public void SendMessage(string toPhone, string message)
      {
        _client.SendMessage(_twilioNumber, toPhone, message);
      }
    }
}
