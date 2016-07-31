using System.ComponentModel.DataAnnotations;

namespace SugarTracker.Web.ViewModels
{
    public class RegisterViewModel
    {
      [Required, MaxLength(256)]
      [EmailAddress]
      public string Email { get; set; }
      [Required, DataType(DataType.Password)]
      public string Password { get; set; }
      [DataType(DataType.Password), Compare(nameof(Password))]
      public string ConfirmPassword { get; set; }
    }
}
