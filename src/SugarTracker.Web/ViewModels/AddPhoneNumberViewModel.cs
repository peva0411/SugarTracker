using System.ComponentModel.DataAnnotations;

namespace SugarTracker.Web.ViewModels
{
    public class AddPhoneNumberViewModel
    {
      [Required]
      [Phone]
      [Display(Name = "Phone Number")]
      public string PhoneNumber { get; set; }
    }
}
