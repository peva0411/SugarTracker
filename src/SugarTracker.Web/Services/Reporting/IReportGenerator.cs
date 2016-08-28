using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SugarTracker.Web.Services.Reporting
{
    public interface IReportGenerator
    {
      FileStream Generate(string userId, DateTime startDate);
    }
}
