using System.Collections.Generic;
using SugarTracker.Web.Entities;

namespace SugarTracker.Web.ViewModels
{
    public class ReadingsViewModel
    {
      public IEnumerable<Reading> Readings { get; set; }
    }
}
