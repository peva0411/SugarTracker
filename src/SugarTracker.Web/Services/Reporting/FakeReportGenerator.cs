using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SugarTracker.Web.Services.Reporting
{
    public class FakeReportGenerator : IReportGenerator
    {
      private readonly string _templatePath;

      public FakeReportGenerator(string templatePath)
      {
        _templatePath = templatePath;
      }

      public FileStream Generate()
      {
        return new FileStream(_templatePath, FileMode.Open);
      }

      public FileStream Generate(string userId, DateTime startDate)
      {
        throw new NotImplementedException();
      }
    }
}
