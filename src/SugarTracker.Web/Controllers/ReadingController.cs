using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SugarTracker.Web.Entities;
using SugarTracker.Web.Models;
using SugarTracker.Web.Services.Repositories;
using SugarTracker.Web.ViewModels;

namespace SugarTracker.Web.Controllers
{
    [Authorize]
    public class ReadingController : Controller
    {
      private readonly IReadingRepository _readingsRepository;
      private readonly UserManager<User> _userManager;

      public ReadingController(IReadingRepository readingsRepository, UserManager<User> userManager)
      {
        _readingsRepository = readingsRepository;
        _userManager = userManager;
      }

      public async Task<IActionResult> Index()
      {
        var user = await _userManager.GetUserAsync(HttpContext.User);

        var readings = _readingsRepository.GetUserReadings(user.Id);

        var viewModel = new ReadingsViewModel{Readings = readings};

        return View(viewModel);
      }
    }
}
