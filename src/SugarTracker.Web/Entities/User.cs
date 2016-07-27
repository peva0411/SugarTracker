using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SugarTracker.Web.Entities
{
    public class User : IdentityUser
    {
      public User()
      {
        UserPhoneNumbers = new List<UserPhoneNumber>();
      }

      public ICollection<UserPhoneNumber> UserPhoneNumbers { get; set; }
    }
}
