using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SugarTracker.Web.Entities
{
    public class User : IdentityUser
    {
      public DateTime DOB { get; set; }
      public string DrName { get; set; }
      public string Medications { get; set; }
     public string DisplayName { get; set; }
      public string DisplayPhone { get; set; }

      public User()
      {
        UserPhoneNumbers = new List<UserPhoneNumber>();
      }

      public ICollection<UserPhoneNumber> UserPhoneNumbers { get; set; }
    }
}
