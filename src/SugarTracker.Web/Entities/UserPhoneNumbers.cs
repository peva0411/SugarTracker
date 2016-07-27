
namespace SugarTracker.Web.Entities
{
    public class UserPhoneNumber
    {
      public int UserPhoneNumberId { get; set; }

      public string UserId { get; set; }

      public string PhoneNumber { get; set; }

      public User User { get; set; }
    }
}
