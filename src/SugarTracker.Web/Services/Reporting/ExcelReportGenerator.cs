using ClosedXML.Excel;
using System;
using System.IO;
using System.Linq;
using SugarTracker.Web.Services.Repositories;

namespace SugarTracker.Web.Services.Reporting
{
  public class ExcelReportGenerator : IReportGenerator
  {
    private readonly string _templatePath;
    private readonly IReadingsService _readingsService;
    private readonly IUserRepository _userRepository;

    public ExcelReportGenerator(string templatePath, IReadingsService readingsService, IUserRepository userRepository)
    {
      _templatePath = templatePath;
      _readingsService = readingsService;
      _userRepository = userRepository;
    }

    public FileStream Generate(string userId, DateTime startDateTime)
    {
      var tempName = Guid.NewGuid();
      var directory = Path.GetDirectoryName(_templatePath);
      var tempFilePath = Path.Combine(directory, tempName.ToString()) + ".xlsx";
      var workbook = new XLWorkbook(_templatePath);
      var workSheet = workbook.Worksheets.FirstOrDefault();
      workbook.SaveAs(tempFilePath);

      var weekView = _readingsService.GetFormattedWeek(userId, startDateTime);
      var user = _userRepository.GetUser(userId);

      workSheet.Cell(2, "A").SetValue(user.DisplayName);
      workSheet.Cell(3, "A").SetValue($"DOB: {user.DOB.ToShortDateString()}");
      workSheet.Cell(4, "A").SetValue($"Phone: {user.DisplayPhone}");
      workSheet.Cell(5, "A").SetValue(user.DrName);

      workSheet.Cell(7, "B").SetValue(user.Medications);

      var readingsStart = 13;

      foreach (var readingViewModel in weekView.Days)
      {
        workSheet.Cell(readingsStart, "A").SetDataType(XLCellValues.DateTime).SetValue(readingViewModel.Date);
        workSheet.Cell(readingsStart, "B").SetValue(readingViewModel.Fasting?.Value);
        workSheet.Cell(readingsStart, "C").SetValue(readingViewModel.Breakfast?.Value);
        workSheet.Cell(readingsStart, "D").SetValue(readingViewModel.Lunch?.Value);
        workSheet.Cell(readingsStart, "E").SetValue(readingViewModel.Dinner?.Value);
       // workSheet.Cell(readingsStart, "F")
         // .SetValue(string.Concat(readingViewModel.AdHocReadings?.Select(s => s.Value), ","));
        readingsStart++;
      }
      workbook.Save();
      return new FileStream(tempFilePath, FileMode.Open);
       
    }
  
}
}
