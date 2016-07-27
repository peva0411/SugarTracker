using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Threading.Tasks;
using SugarTracker.Web.Entities;

namespace SugarTracker.Web.ViewModels
{
    public class ManageViewModel
    {
      public IList<UserPhoneNumber> UserPhoneNumbers { get; set; }
    }
}
