using System;
using Microsoft.AspNetCore.Mvc;
using SugarTracker.Web.Services.Reporting;

namespace SugarTracker.Web.Controllers.Api
{
  [Route("api/[Controller]")]
  public class ReportsController : Controller
  {
    private readonly IReportGenerator _reportGenerator;

    public ReportsController(IReportGenerator reportGenerator)
    {
      _reportGenerator = reportGenerator;
    }

    [HttpGet]
    [Route("{weekStart}/{userId}")]
    public IActionResult Get(DateTime weekStart, string userId)
    {
      var report = _reportGenerator.Generate(userId, weekStart);
      return File(report, "application/excel", $"{weekStart.ToShortDateString()}.xlsx");
    }

  }
}
